podTemplate(label: 'dotnet', containers: [
    containerTemplate(name: 'jnlp', image: 'jenkinsci/jnlp-slave:alpine', ttyEnabled: true, args: '${computer.jnlpmac} ${computer.name}'),
    containerTemplate(name: 'dotnet', image: 'microsoft/dotnet:1.1.1-sdk', ttyEnabled: true, command: '/bin/sh -c', args: 'cat'),
    containerTemplate(name: 'docker', image: "${env.PTCS_DOCKER_REGISTRY}/docker-client:latest", ttyEnabled: true, command: '/bin/sh -c', args: 'cat')
  ]) {

docker.withRegistry("${env.PTCS_DOCKER_REGISTRY}") {
    node('dotnet') {
      stage('Checkout') {
	checkout scm
      }

      stage('Build') {
	container('dotnet') {
	  sh """
	  dotnet restore
	  dotnet build -c Release -o out
	  """
	}
      }
  /*    stage('Test') {
	container('dotnet') {
	  sh """
	  dotnet test
	  """
	}
      }*/
      stage('Package') {
        container('docker') {
          def image = docker.build("${env.PTCS_DOCKER_REGISTRY}/citd-backend:dev", '.')
          image.push()
        }
      }
    }
  }
}
