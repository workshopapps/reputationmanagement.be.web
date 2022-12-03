String githubUrl = "https://github.com/workshopapps/reputationmanagement.be.web.git"

node () {
    stage('Checkout') {
        checkout([
            $class: 'GitSCM', 
            branches: [[name: 'development']], 
            doGenerateSubmoduleConfigurations: false, 
            extensions: [], 
            submoduleCfg: [], 
            userRemoteConfigs: [[url: """ "${githubUrl}" """]]])
    }
    stage('Build') {
        bat """
        
        dotnet build -c Release /p:Version=${BUILD_NUMBER}
        dotnet publish -c Release --no-build
        """
    }
    stage('Deploy'){
        sh "sudo cp -rf ${WORKSPACE}/backend/* /home/enyioman/repute_api/backend"
		sh "sudo su - enyioman && whoami"
		sh "sudo dotnet run"
    }
}


// pipeline {

// 	agent any
// 	stages {
		
//         stage("build backend"){

// 			steps {
// 				sh "dotnet tool install --global dotnet-ef --version 6.*"
// 			} 
//         }
// 		stage("deploy") {
		
// 			steps {
// 				sh "sudo cp -rf ${WORKSPACE}/backend/* /home/enyioman/repute_api/backend"
// 				sh "sudo su - enyioman && whoami"
// 				sh "sudo dotnet run"
// 			}
			
// 	}


// 	}



// }

