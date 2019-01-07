
# Hello And Welcome To Ensayar!

With this app you may create a visual representation of the music gear that you have.

Let's get started!

## Launching The App

Let's get the app set up first.

1. Clone the git repository
```
git clone https://github.com/kellycook301/finalCapstone
```

2. Run the project in the root directory
```
start RealRehearsalSpace.sln
```

3. Launch the project
```
cd RealRehearsalSpace
```

Go ahead and create an account by clicking on "Register As A New User." Once you create an account you can go ahead and log into your page.
Here you will see a landing page. The calendar icon will let you book a room and the account icon will take you to your list of booked room. At first the list of your booked rooms will be blank. Let's change that!

## Booking A Room
To book your first room click on the calendar icon. You will be taken to a page where you may select a room that you want to book. After selecting a room you will be presented with a dropdown menu asking for what time you would like to book. Times taken in that room by another user will not be present. After booking the room it will be added to your list of booked rooms! You may see the list by clicking on the account button in the bottom right-hand corner.

## Editing The Time
Let's say that you want to change the time you would like to be in the room. In the list of your booked rooms click on the "pencil" icon. You will be presented with a view where you can change and save the time. Once again, the times that are taken will not be listed.

## Deleting A Post
Now let's say you would like to cancel that time alltogether. In the list of your booked times, click on the "trash can" icon. You will be presented with a view asking if you would like to cancel. Confirm by selecting the "cancel session" button.

## Logging Out
Whenever you would like to log out you may click on the "hamburger" icon in the upper right-hand corner and select "logout."

------

## The Process
Everything below details the process of how the app was created. All in all it was a fun and challenging process. I was thinking of ideas for what to do for my capstone. I remember Brenda (our main instructor for the front-end development class) saying that we should work on something that we enjoy. Because if your project isn't on something you're passionate about, then it becomes more of a chore. I remember browsing for icons on IconScout and found some PNGs of instruments. I then thought about making an app that will show a list of the music gear that you own and these icons could be displayed in cards for each entry. I then drew out how I would want the app to look (see below).

![alt text](https://github.com/kellycook301/GearList/blob/master/App%20Layout%201_final2.JPG)

I knew we'd have to have a login page. I originally wanted to have separate sign in and register cards but the login proved to be quite a challenge, so I condensed it into a single sign in/register card. Fun!

![alt text](https://github.com/kellycook301/GearList/blob/master/App%20Layout%202_final2.JPG)

![alt text](https://github.com/kellycook301/GearList/blob/master/App%20Layout%203_final2.JPG)

![alt text](https://github.com/kellycook301/GearList/blob/master/App%20Layout%204_final.jpg)

The first day was spent trying to tackle the login portion of the process. I floundered the first day and started to work on the actual app the next day. I wanted to get the basic layout with the "add gear" button on the page. What I really wanted to implement were some forms contained within modals. Thankfully, ReactStrap had just what I needed. Because the instruments all have different criteria, I had to make a LOT of different forms. So there was going to be a lot of data passed in. I started with only a select few kinds of gear but realized I needed more for variety's sake. By the end of the first week I had pretty much met MVP. Data was getting passed in and populated to the DOM in card form. I was very happy. I just wanted to tighten up some of the things.

I ran into some trouble with setting state in some areas. I was trying to figure out a cool way to have it work with the delete function. At first when you clicked on the trashcan icon to delete the post you would be prompted with the "localhost:3000 Would you like to delete this post?" thing and it would delete the post but it would still show it until the page refreshed. I found something to force reload the page, but hated the functionality of it. Because we're using React, it should just update state. I didn't end up fixing that until week three. By the end of week two I had pretty much fully met MVP. The login was working and you could easily edit and delete posts. One of my stretch goals was implementing different users. The following week proved to be very mentally challenging.

During week three I came in to have someone guide me through the process of implementing different users. Meg, a teaching assistant, said it would be easier than I thought. Turns out my project was a little wonky and a LOT of bandaids needed to be placed onto my project to get it up and running the way I wanted. Serious shouts out to Brenda, Jenna, Meg, and Steve for their help and guidance. At one point I thought that I would be presenting my MVP project. Thankfully, it was all working the way I wanted.

One thing I did to fix the force reload of the page was actually implement a delete modal. When you click on the trashcan a modal pops up with a message asking if you want to have the post deleted. You may choose to cancel or delete the post and it updates state. I also have a similar post applied to when you add something to your list. Instead of the "localhost:3000" message popping up, a modal appears confirming the addition of your post. I thought it would just look better that way.

And here we are! All done. Very happy with the project even though there's some small stuff I wanted to have added. Here's a list of the plugins and things I used in my project.

* MVC for the UI
* Iconscout for the various icons and images.
* Font Awesome for the trash can, pencil, and details icons

Shouts out to...
* My Cohort for always wanting to see me succeed and being all around awesome!
* My instructors Andy, Steve, Meg, and Kimmy! They were always willing to help when I was in a rut.
* Coffee for keeping me ENERGIZED.
* My my wife Jorden for believing me and always picking me up when I felt lost. My daughter Leven for being my main source of drive and inspiration. My parents for always being there. Thank you thank you thank you!
