node {
  docker.withRegistry("${env.PTCS_DOCKER_REGISTRY}", "${env.PTCS_DOCKER_REGISTRY_KEY}") {
    stage 'Checkout'
      checkout scm

    stage 'Build'
      container('dotnet') {
        sh """
	dotnet restore
	dotnet build -c Release -o out
    	"""
      }
    stage 'Test'
      container('dotnet') {
        sh """
        dotnet test
        """
      }
    stage 'Package'
      def image = docker.build("${env.PTCS_DOCKER_REGISTRY}/citd-backend:dev", '.')
      image.push()
  }
}
