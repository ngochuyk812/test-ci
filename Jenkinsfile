pipeline {
    agent none
    stages {
        stage("build") {
            agent any
            environment {
                DOCKER_TAG = "1232123"
                DOCKER_IMAGE = "ngochuyk8/webapi"
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
            agent any // Bạn có thể chọn agent ở đây
            steps {
                script {
                    echo 'Pulling the image...'
                    sh "docker pull ${DOCKER_IMAGE}:${DOCKER_TAG}"
                    
                    echo 'Running the container...'
                    sh "docker run -d --name my_container ${DOCKER_IMAGE}:${DOCKER_TAG}"
                }
            }
        }
    }
}
