# khnumdev.TwitBot
Project for having fun with Microsoft Bot Framework and Twitter user's timeline. The bot includes:
- Service to ingest from Twitter
- Services to ingest content into a little multidimensional code
- Connectivity to some APIs of Cognitive Services's

HOW TO INSTALL
--------------

1. You need to have an Azure Subscription. Get a new one if you don't have it.
2. You need to have a Twitter Developer account. Get a new one and obtain the credentials to authenticate to the API using OAUTH without user intervention.
3. Download the project. There is an ARM template with all the required stuff to deploy the project.
	1. In the "azuredeploy.json" file, update the variable "defaultname" with the name that you want to the bot and the environment. By default the environment is the sufix "dev", "test" or "" (empty) for production.
	2. Go to [Microsoft Bot Framework](https://dev.botframework.com/) and create a bot, where the endpoint will be your "defaultname" plus "azurewebsites.net/". For example, if "defaultname" is "mybotdev", the endpoint to register is "http://mybotdev.azurewebsites.net/".
	3. Once the bot is created in Bot Framework, get the MicrosoftAppID and MicrosoftAppPassword.
	4. Then, go to [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/en-us/apis), create an account and subscribe to L.U.I.S service.
	5. Finally, get back to the project and open the file "azuredeploy.parameters.json". Update it with the values for Twitter, Microsoft Apps and Cognitive L.U.I.S. services.
4. Deploy the template in your subscription. It will create all the required resources to use your bot in Azure/production.
5. To use the bot in your local machine, you will need to put some values in the local configuration. Go to src/Khnumdev.Twitbot/Configurations:
	1. Connections.config. You will need to provide an Azure Storage Account if you want to run the webjobs locally. In addition, you should have an instance in localhost of SQL Server to create and use the databases.
	2. Settings.config: Populate the same values that you have populated in the "azuredeploy.parameters.json" with a few more. You can specify a initial Twitter user Id to load content (using the TwitterUserId setting) and you will to provide the TextAnalytics key. If you have deployed the project with the ARM template, go to your Azure Resource Group and get the key from the Text Analytics API.

HOW TO USE IT
-------------

The bot will populate the timeline of the users that are inserted in the [Twitbot.Core].[dbo].[TwitterUsers] table. Just put a Twitter userId and the name in that table and the webjob will ingest all the content.

Then, you will need to build your own L.U.I.S. model. Go to Cognitive Services and create all the entities you need for your model. Then, you should train it a bit.

Finally, you can modify or update the way the bot choose the tweet to answer. Check the file "MessageMatcherProcessor.cs" inside of "khnumdev.TwitBot" project.

Happy coding!!

PD: If you want to know more about this project, check the following [post](http://geeks.ms/aperez/2016/11/09/buscando-la-felicidad-con-bot-framework-y-cognitive-services/) -in spanish- for further information.



