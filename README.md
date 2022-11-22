<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[Contributors][contributors-url]

[Forks][forks-url]

[Stargazers][stars-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/github_username/repo_name">
    <img src="https://icons8.com/icon/XPQeTFPzdCTD/webpage" alt="Logo" width="80" height="80">
  </a> -->

# REPUTATION MANAGEMENT

  <p align="center">
    "Reputation Management. Imagine you have a restaurant. Someone comes and leaves a horrible review and 1 star. You need that review gone. That's what we are here for. Our lawyers will send a letter to the reviewer asking them to take down their review. If successful, you pay us for the service."
    <br />
    <a href="https://drive.google.com/file/d/1a6jXVKLDQ3smRvZqMDSd4edPNNTRHrWj/view?usp=sharing"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://fixit.hng.tech/">View Demo</a>
    ·
    <!-- <a href="https://github.com/github_username/repo_name/issues">Report Bug</a>
    ·
    <a href="https://github.com/github_username/repo_name/issues">Request Feature</a> -->
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->

This is an application mainly for the customer and managed by the lawyers. This products focuses to keep and manage a customer or user's reputation which a lawyer registered ensure bad reviews relating to the cutomer are removed and keep the customer business safe with less worries on bad reviews.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* C#
* ASP.NET Core
* React js

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

* .NET 6
* IDE (Code Editor)
    * Visual Studio 2022 or
    * Visual Studio 2019 Code


### Installation

1. Get the repo link at [https://github.com/workshopapps/reputationmanagement.be.web](https://github.com/workshopapps/reputationmanagement.be.web)
2. Clone the repo
   ```sh
   git clone https://github.com/workshopapps/reputationmanagement.be.web.git
   ```
3. Switch to the development branch (this is for developers)*
4. Open the project folder, in the root path of the project, you will see a .sln file (src/src.sln)
5. To open the .sln file, you need Visual Studio 2022 to run the .sln file. To use the Visual Studio Code editor check from list number 7
6. With the Visual Studio 2022
      * Open the .sln file using Visual Studio 2022
      * In the terminal or cmd command shell run the following command to install ef:
        ```sh
        dotnet tool install --global dotnet-ef --version 6.*
        ```
        * Run the database contexts by running the following commands in the command shell:
        ```sh
        dotnet ef database update --context ApplicationDbContext
        ```
        ```sh
        dotnet ef database update --context AppIdentityDbContext
        ```
      * Click the Start or Play button to run your endpoints
      * A Swagger Doc is opened, displaying every enpoints, select and test your endpoints
7.  Using the Visual Studio Code Editor
      * Install the [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
      * Next install the [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download), ensure you install the .NET 6.0 Version
      * Open a terminal/command prompt and navigate to the project folder where you will run the app.
      * When the project folder is first opened in VS Code: A "Required assets to build and debug are missing. Add them?" notification appears at the bottom right of the window.
      * Select "Yes"
      * In the terminal or cmd command shell run the following command to install ef:
        ```sh
        dotnet tool install --global dotnet-ef --version 6.*
        ```
        * Run the database contexts by running the following commands in the command shell:
        ```sh
        dotnet ef database update --context ApplicationDbContext
        ```
        ```sh
        dotnet ef database update --context AppIdentityDbContext
        ```
      * Run the app by entering the following command in the command shell:
        ```sh
        dotnet run
        ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

David Okeke - davidokeke.c@gmail.com

Project Link: [https://github.com/workshopapps/reputationmanagement.be.web](https://github.com/workshopapps/reputationmanagement.be.web)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Setup .NET app in Visual Studio Code](https://code.visualstudio.com/docs/languages/dotnet)
<!-- * []()
* []() -->

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-url]: https://github.com/workshopapps/reputationmanagement.be.web/graphs/contributors
[forks-url]: https://github.com/workshopapps/reputationmanagement.be.web/network/members
[stars-url]: https://github.com/workshopapps/reputationmanagement.be.web/stargazers
