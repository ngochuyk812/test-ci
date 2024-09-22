pipeline {
    agent none
    environment {
        DOCKER_IMAGE = "ngochuyk8/webapi"
    }
    stages {
        stage("Build") {
            agent any
            environment {
                // Sử dụng tên nhánh và 8 ký tự đầu tiên của commit ID
                DOCKER_TAG = "${env.GIT_COMMIT.take(8)}"
            }
            steps {
                script {
                    echo "Docker Tag: ${DOCKER_TAG}"
                }
                sh "docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} ."
                sh "docker tag ${DOCKER_IMAGE}:${DOCKER_TAG} ${DOCKER_IMAGE}:latest"
                sh "docker image ls | grep ${DOCKER_IMAGE}"
                withCredentials([usernamePassword(credentialsId: 'docker-hub', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                    sh 'echo $DOCKER_PASSWORD | docker login --username $DOCKER_USERNAME --password-stdin'
                    sh "docker push ${DOCKER_IMAGE}:${DOCKER_TAG}"
                    sh "docker push ${DOCKER_IMAGE}:latest"
                }
                // Clean to save disk space
                sh "docker image rm ${DOCKER_IMAGE}:${DOCKER_TAG}"
                sh "docker image rm ${DOCKER_IMAGE}:latest"
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            agent any 
            steps {
                script {
                    echo 'Removing existing container if it exists...'
                    sh """
                    if [ \$(docker ps -aq -f name=webapi) ]; then
                        docker rm -f webapi
                    fi
                    """
                    
                    echo 'Pulling the image...'
                    sh "docker pull ${DOCKER_IMAGE}:latest"
                    
                    echo 'Running the container...'
                    sh "docker run -d --name webapi -p 80:8080 ${DOCKER_IMAGE}:latest"
                }
            }
        }
    }
}
