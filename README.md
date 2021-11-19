          

# Hardwork_Simulator

This is the source code of the game originally developed for LudumDare 49 (theme: Unstable). The game mimics an unstable text editor in which you pretend to be typing while dropping letters to kill stickmen. Have fun analyzing the (bad) code of a self-taught programmer developing a game in 48 hours, without sleep, and drinking a lot of coffee.

It turned into a [Steam game](https://store.steampowered.com/app/1781880/Hardwork_Simulator) with lots of extra features. Check it out :)

# BONUS: How to read the Steam reviews from all your games using a RSS feed!

In the steam version of Hardwork Simulator I made the whole "level" come from a txt file (which can be customized by the player). I added many customizable features for this txt, such as the possibility to read an rss feed. With this implemented, a friend gave me the idea of making a level with TESTIMONIALS, based on the rss feed of the steam reviews of the game.

One thing leads to another and I thought: “why not make an rss feed aggregator of all my games?!”

## Why?

I currently manage 3 games on steam. I don't know about you, but sometimes it's a pain in the ass to go into each one and try to see what reviews I haven't read yet.

## What?

If you have knowledge of HTML, I think that the mere mention of the idea is enough for you to assemble the rss feed of your games by yourself. But if, like me, you don't have any knowledge of html and css, the process is a bit boring. I went through it and I will describe here how to configure everything to your liking.

## How?

You will need one of those sites that convert any site into an rss feed. I've had success with fetchrss.com and fivefilters.org using almost the same process. But I decided to use fetchrss for myself and I ll use this one to explain. Follow these steps:
- Go to your game's Community Hub, select reviews, and choose the filter you want. I think it makes more sense to choose "Most Recent", "All" and "All Languages". But you might want to make a feed only with negative reviews, for example. Copy the URL with the filter applied;
- Open fetchrss.com and paste the URL;
- At the page that will appear, fill these fields:
	- New Items => div.apphub_Card
	- Headline => div.apphub_CardContentAuthorName
	- Summary => div.apphub_UserReviewCardContent (you can use div.apphub_CardTextContent instead for a cleaner entry)
- Click Generate Feed and copy the xml link;
- Repeat the steps above for each game;
- Now choose a rss reader of your choice (I chose feedly.com);
- Click Create New Feed. Call it "My Awesome Games' reviews";
- Click Add Content and paste the first xml. Repeat the process for each xml;
- You are done! The result in the Feedly app is something like this:
	- <img src="https://i.imgur.com/pemMuIH.png" alt="" width="200"/>
	- You can click on each entry to read the review.
	
## Limitations
	
The fetchrss free plan has some limitations you must be aware of:
	![](https://i.imgur.com/lbBGP7g.png)
So, with free plan...
- You can only have rss for 5 games;
- It will be updated once a day;
- If your game recieve more than 5 reviews on that day, it will probably skip some;
- If you don't get a new review within 7 days, you will need to recreate it (or is it 7 days without updating the feed from feedly?)

I'm using the trial of the Basic plan and will decide afterwards if I downgrade to Free.

## Thanks
That's it! I hope this is useful for anyone.


