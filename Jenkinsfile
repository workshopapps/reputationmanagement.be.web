pipeline {

	agent any
	stages {
        
        stage('Build') {
            steps {
                
                sh "dotnet tool install --global dotnet-ef --version 6.*"
                sh "dotnet build -c Release /p:Version=${BUILD_NUMBER}"
                sh "dotnet publish -c Release --no-build"
                
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

