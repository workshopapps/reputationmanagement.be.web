pipeline {

	agent any
	stages {
		
        stage("build backend"){

			steps {
				sh "dotnet tool install --global dotnet-ef --version 6.*"
			} 
        }
		stage("deploy") {
		
			steps {
				sh "sudo cp -rf ${WORKSPACE}/backend/* /home/enyioman/repute_api/backend"
				sh "sudo su - enyioman && whoami"
				sh "sudo dotnet run"
			}
			
	}


	}



}
