pipeline {

	agent any
	stages {
        
        stage('Build') {
            steps {
                bat """
                dotnet tool install --global dotnet-ef --version 6.*
                dotnet build -c Release /p:Version=${BUILD_NUMBER}
                 dotnet publish -c Release --no-build
                """
            }
        
    }
		stage("Deploy") {
		
			steps {
				sh "sudo cp -rf ${WORKSPACE}/backend/* /home/enyioman/repute_api/backend"
				sh "sudo su - enyioman && whoami"
				sh "sudo dotnet run"
			}
			
	}


	}



}

