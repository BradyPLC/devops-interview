## DevOps Engineer - technical interview

### Testing goals
With this test, we want to see your ability to create a CI/CD pipeline and supporting infrastructure from scratch, as well as your skills as a system administrator.

### The application

The app is a simple weather lookup by user entered locations. 

- The app is built with dotnet core sdk version 3.1
- The app uses [AccuWeather API](https://developer.accuweather.com/accuweather-locations-api/apis) for lcoations lookup
  -  You will need to create a free account with [AccuWeather](https://developer.accuweather.com/) in order to register an application and call their API.
- The startup application is **BradyWeather.Blazor.Server.csproj**

At the end of the exercise the application should look like below.  

![Blazor Weather](Docs/BlazorWeather.gif)

### The task
Your task is to automate the deployment and provisioning of resources that host the app provided.
It should be hosted in a highly available and scalable web application hosted in **Azure**. A free account can be created [here](https://azure.microsoft.com/en-gb/free/)
for Azure.  Should not have to spend any money to get complete the exercise. 

Your CI job should:
- Investigate and understand which values need to be swapped as part of CICD pipeline. This needs to be done for the application to work :)  
- Run when a feature branch is pushed to Github (you should fork this repository to your Github account). 
- Deploy to a target environment when the job is successful.
- A clean and minimal working infrastructure is preferred. 
- Consider security.
- Consider tests at all levels. 

### Submission
- Create a public Github repository and fork the Brady repository into it. 
- Your solution should be pushed here.  Please do not submit PRs back to the main repository.
- Include a README which desribes how the CI pipeline and any IAC works
- A link the site that has been deployed. 

### Bonus Points
- Commit often - would rather see a history of trial and error than a single push. 
- Versioning of the deployment
- Write some tests for code and integrate into pipeline