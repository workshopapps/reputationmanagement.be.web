pipeline {
	
	environment {
        CI = 'false'
    	}

	agent any
	stages {
        
	stage("Get repo"){

			steps {
				sh "rm -rf ${WORKSPACE}/reputationmanagement.be.web"
				sh "git clone -b development https://github.com/workshopapps/reputationmanagement.be.web.git"
				sh "sudo cp -r ${WORKSPACE}/reputationmanagement.be.web /home/ehmeeops/reputationmanagement.be.web"
			}
		}
	
        	stage('Build') {
			steps {

				sh "dotnet tool install --global dotnet-ef --version 6.*"
				sh "dotnet build -c Release /p:Version=${BUILD_NUMBER}"
				sh "dotnet publish -c Release --no-build"

				}
		}

		stage("test backend"){

			steps {
				sh "cd reputationmanagement.be.web"
				sh "cd reputationmanagement.be.web/src && dotnet test src/ -c Release --no-restore --no-build"
			}
        	}
		
		stage("Deploy") {
		
			steps {
				sh "sudo cp -rf ${WORKSPACE}/src/* /home/ehmeeops/reputationmanagement.be.web/backend"
				// sh "sudo pm2 start"
			}		
		}
	}
}
