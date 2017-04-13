podTemplate(label: 'dotnet', 
  containers: [
    containerTemplate(name: 'jnlp', image: 'jenkinsci/jnlp-slave:alpine', ttyEnabled: true, args: '${computer.jnlpmac} ${computer.name}'),
    containerTemplate(name: 'dotnet', image: 'microsoft/dotnet:1.1.1-sdk', ttyEnabled: true, command: '/bin/sh -c', args: 'cat'),
    containerTemplate(name: 'docker', image: 'ptcos/docker-client:latest', alwaysPullImage: true, ttyEnabled: true, command: '/bin/sh -c', args: 'cat')
  ],
  volumes: [
    hostPathVolume(hostPath: '/var/run/docker.sock', mountPath: '/var/run/docker.sock')
  ]
) {

  docker.withRegistry("https://eu.gcr.io") {
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
          //Workaround Jenkins bug https://issues.jenkins-ci.org/browse/JENKINS-31507
          //def image = docker.build("${env.PTCS_DOCKER_REGISTRY}/citd-backend", '.')
          sh """
          docker build -t ${env.PTCS_DOCKER_REGISTRY}/citd-backend .
          """
          def image = docker.image("${env.PTCS_DOCKER_REGISTRY}/citd-backend")
          image.push('dev')
        }
      }
    }
  }
}
