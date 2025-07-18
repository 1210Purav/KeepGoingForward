							---- Keep Going Forward ----

---- Introduction ----

Hello. Thank you for being here.

This is my official first game that I have made. I have worked on other projects before but they were mostly made by following tutorials and I've never actually given them to anyone. This README file will contain all the info about the game. Starting from the controls to also what the game is about the context, why I made  game like this, how the game currently functions, future plans for this game. You can absolutely choose to ignore any part of the README file but I request you to read the following controls part so that you can play the game.

----  Controls ----

The game is simple to play, there are no deep mechanics
use 
the W key to go forward
the S key to go backward
the A key to go left
the D key to go right 

that's it. 


---- About ----

The game is meant to represent a nightmare that I used to have during the COVID pandemic phase, it doesn't have any complex systems and no crazy graphics, explosions or even what you usually find in other AAA and indie titles. It is a simple project that is expression of one of my most darkest times. It may not be relatable to everyone and even if it is I don't think anyone would find it interesting. This is my very first experience in making an actual game by myself and not blindly following a tutorial so I tried to make the most of it to the best of my current ability.

In the Game you play as a simple red ball and all you have to do is keep going forward. You have a spotlight above you that is meant to show you a few steps further but not much. aside from that there is a dark void ahead of you and there are whispers that are trying to; in one way or another keep you from going ahead. Amidst all the negative whispers there's one or two positive ones that tell you to go ahead not matter what (I have included words as well so that you may understand what the whispers are saying) after a certain point the whispers stop signifying that you have moved on / grown past them / proven them wrong / anything, it's upto your imagination of what the "silence" means. The void still remains but in context of my own nightmare that silence represents peace to me, that I was no longer haunted by those whispers. 

---- Logic (written in pseudocode meant for developers)----

// --- INITIALIZATION ---
On Game Start:
    Pause game time (Time.timeScale = 0)
    Show Main Menu
    Disable WhisperTextController GameObject

// --- MAIN MENU FLOW ---
If player clicks 'Start Game':
    Hide Main Menu
    Resume game time (Time.timeScale = 1)
    Enable WhisperTextController GameObject
    Call StartAudioCycle() // Triggers after delay

// --- WHISPER TEXT CONTROLLER LOGIC ---
StartAudioCycle():
    Record globalStartTime = current time
    Wait for ActualstartTimeInSeconds (e.g., 30s)
    Unpause Whisper Logic
    Record audioStartTime = current time
    Play whisper audio
    Start text display logic

In Update():
    If game is paused → return
    If Time.time - globalStartTime > endTimeInSeconds:
        Call StopWhispers() // Stop audio & text permanently
        return

    elapsedTime = Time.time - audioStartTime
    If elapsedTime >= next text trigger time:
        Display next shuffled whisper text (with fade in/out)
    If elapsedTime >= audioDuration:
        Pause for cyclePause seconds
        Restart whisper cycle (loop until endTimeInSeconds)

// --- PLAYER INPUT & GAME STATES ---
If 'Escape' pressed and game is not paused:
    Pause game
    Show Pause Menu
    Call PauseWhispers()

If 'Resume' selected:
    Resume game
    Call ResumeWhispers()

If 'Quit to Menu' selected:
    Stop all whisper audio and text
    Disable WhisperTextController
    Show Main Menu
    Pause game time

// --- AUDIO TRANSITION (optional for future) ---
If elapsedTime < fadeInDuration:
    Gradually increase volume

If nearing endTimeInSeconds:
    Gradually reduce volume
    Fade out text more softly

// --- PLAYER COMFORT TRANSITION ---
After whispers end:
    Introduce subtle ambient tone or visual change
    Signify shift from discomfort to peace


---- Future plans ----

I do plan on improving the game in the future I have a few improvements in mind that I haven't implemented in this version of build due to time constraints (I am currently in my Final Year of Engineering) but whenever I get time I will keep working on it making it better but as it stands, the game very closely represents my nightmare and once again it might not be a fun experience for you and you may most likely get bored because there's literally nothing in front of you but, I still thank you for playing this game and I will improve with every project and maybe one day I will be able to make a game that is loved by many

Thank you once again.  

    