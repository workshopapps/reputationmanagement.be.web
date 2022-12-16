pipeline {

  agent any
  
  stages {
    
    stage("Build Backend"){
      
      steps{
            sh "cd src && dotnet build"
	    sh "cd src && dotnet publish"
      
      }
    
    }
	  
    stage("test backend"){

	steps {
		//sh "cd reputationmanagement.be.web"
		sh "cd src && dotnet test"
	 	}
     }
    
    stage("Deploy App"){
      
      steps{
	    sh "sudo cp -rf ${WORKSPACE}/src/* /home/ehmeeops/reputationmanagement.be.web/src/"
            sh "sudo systemctl restart reputeapi.service"
      }
    }
  
  } 
  
  post{
    failure{
        emailext attachLog: true, 
        to: 'mhizxeryl@gmail.com@gmail.com',
        subject: '${BUILD_TAG} Build failed',
        body: '${BUILD_TAG} Build Failed \nMore Info can be found here: ${BUILD_URL} or in the log file below'
    }
  }
  
}

