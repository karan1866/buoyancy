//Script

//Declarations
var speechurl = "http://translate.google.com/translate_tts?q=";
var progress_value = -1;
var t;
var boardType = "menu";
var sample = "";

//Story Board
var timer;
var storyLine = 0;
var newUser = true;

var menu = new Array("bbc news|/tts/key/bbc.mp3", "calculator|/tts/key/calc.mp3", "currency convert|/tts/key/curr.mp3", "spell check|/tts/key/spel.mp3", "time|/tts/key/time.mp3", "weather|/tts/key/wthr.mp3" ,"wikipedia|/tts/key/wiki.mp3");

var player = new Array('resume|/tts/key/rsum.mp3', 'rewind|/tts/key/rew.mp3', 'forward|/tts/key/forw.mp3', 'restart|/tts/key/rstr.mp3', 'exit|/tts/key/exit.mp3');

function noteEvent(id)
{
	document.getElementById("td1").style.backgroundColor="#777777";
	document.getElementById("td2").style.backgroundColor="#777777";
	document.getElementById("td3").style.backgroundColor="#777777";
	document.getElementById("td" + id).style.backgroundColor="#ffffff";
	
	document.getElementById("div1").style.visibility="hidden";
	document.getElementById("div2").style.visibility="hidden";
	document.getElementById("div3").style.visibility="hidden";
	document.getElementById("div" + id).style.visibility="visible";
}

function ignoreEvent(e,type) {

    if (plugin.loadState != 0) {
        var x;
        if (navigator.appName == "Netscape") {
            x = e.clientX;
        }
        else {
            x = event.clientX;
        }

        if (plugin.keyboard.isLocked == false) {
            if (plugin.keyboard.isTyping == false) {

                if (plugin.keyboard.size() == 1) {

                    var left = x - (x / document.body.clientWidth) * (document.body.clientWidth - (75 * plugin.keyboard.totalKeys));

                    var index = parseInt(left / 75);
                    if (left / 75 > index)
                        index += 1;
                }
                else {
                    var index = document.body.clientWidth / (document.body.clientWidth / plugin.keyboard.totalKeys);

                    if (parseInt(index) < index)
                        index = parseInt(index + 1);
                }

                index -= 1;


                if (type == "move") {
                    if (plugin.keyboard.selectedIndex != index)
                        plugin.keyboard.buttonMouseOver(document.getElementById(plugin.keyContainerId).childNodes[index].childNodes[0], index);

                    document.getElementById(plugin.keyContainerId).childNodes[index].childNodes[0].style.backgroundColor = "#3e95ff";
                }
                else {
                    plugin.keyboard.buttonClick(document.getElementById(plugin.keyContainerId).childNodes[index].childNodes[0], index);
                }
            }
            else if (document.getElementById("lockButton").style.visibility == "hidden") {

                if (type == "move") {
                    document.getElementById("lockButton4").onmouseover();
                }
                else {
                    document.getElementById("specialButton4").onclick();
                }
            }
        }
    }
}

function lockButtons(id, value) {
    if (id == 0)
        document.getElementById("lockButton").style.visibility = value;
    else
        document.getElementById("lockButton" + id).style.visibility = value;
}

function buttonPronunciation(str) {
    if (!str.split("|")[1]) {
        return str.split("|")[0];
    } else {
        return str.split("|")[1];
    }
}

function buttonText(str) {
    return str.split("|")[0];
}

function virtualButtonClicked(button, index) {
    switch (boardType) {
        case "menu":
            appClick(button, index);
            break;
    }
}

function pageInit() {

    

    plugin.onLoad = pluginLoaded;
    lockButtons(0, "hidden");
    lockButtons(1, "hidden");
    lockButtons(2, "hidden");
    lockButtons(3, "hidden");
    lockButtons(4, "hidden");
    //plugin.onError = pluginError;

    //document.getElementById('content').innerHTML = "hold escape for 4 seconds to switch to visual mode" + document.getElementById('content').innerHTML;

    //$("#progressbar").css( "left" , document.body.clientWidth - 216);

    //document.onkeydown = keydown;
    //document.onkeyup = keyup;

}

function pluginLoaded() {
    //plugin.keyboard.addKeys(menu);
    plugin.keyboard.hide();
    //plugin.keyboard.lock();
    document.onclick = function() {
        start();
    };

    //plugin.keyboard.key_Clicked = virtualButtonClicked;
    document['black_flashplugin'].readLocal(new Array("/tts/2.mp3", 400 ,"/tts/0.mp3"));
}

function start() {
	document.getElementById("notebg").style.visibility="hidden";
	document.getElementById("div1").style.visibility="hidden";
	document.getElementById("div2").style.visibility="hidden";
	document.getElementById("div3").style.visibility="hidden";
	plugin.keyboard.hide();
    //plugin.keyboard.lock();
    document.onclick = function() {
        if (storyLine == 0) {
            newUser = false;
            clearInterval(timer);
            reset();
            document['black_flashplugin'].readLocal("/tts/1.mp3", "initBlack");
            //+storyLine=2;
        }
    };

    //plugin.keyboard.key_Clicked = virtualButtonClicked;
    document['black_flashplugin'].readLocal("/tts/3.mp3","startInitClock");

}

function initBlack() {
    plugin.keyboard.key_Clicked = virtualButtonClicked;
    plugin.keyboard.show();
    plugin.keyboard.unLock();
    storyLine = -1;
    reset();
    plugin.keyboard.addKeys(menu);
    boardType = "menu";
    plugin.keyboard.key_Clicked = virtualButtonClicked;
    plugin.keyboard.key_MouseOver = function(button, index) {
        document['black_flashplugin'].readLocal(buttonPronunciation(menu[index]));
    };
}

function reset() {
    document.onclick = function() { };
    plugin.keyboard.closeKeyboardInput();
    plugin.keyboard.clearKeys();
    plugin.keyboard.onKeyboardSubmit = function(text) { };         //when submit button is clicked
    plugin.keyboard.onKeyboardExit = function() { };               //when exit button is clickedss
    plugin.keyboard.key_MouseOver = function(button, index) { };   //When mouse moves over a key
    plugin.keyboard.key_MouseOut = function(button, index) { };    //When mouse moves out of a key
    plugin.keyboard.key_Clicked = function(button, index) { };     //When a key is clicked
    plugin.keyboard.onTextInput = function(key, value) { }; 
}




//Tutorial functioning

function TutorialStoryBoard(button, index) {
    switch (storyLine) {
        case 0:
            storyLine = 1;

            plugin.keyboard.key_Clicked = TutorialStoryBoard;
            plugin.keyboard.clearKeys();
            plugin.keyboard.lock();
            plugin.keyboard.addKeys(menu);
            plugin.keyboard.show();

            plugin.keyboard.key_MouseOver = function(button, index) {
                document['black_flashplugin'].readLocal(buttonPronunciation(menu[index]));
            };

            document['black_flashplugin'].readLocal(new Array("/tts/tut/intro/0_1.mp3", 200, "/tts/tut/intro/0_2.mp3", 300, "/tts/tut/intro/0_3.mp3", 300, "/tts/tut/intro/0_4.mp3", 600, "/tts/tut/intro/0_5.mp3", 400, "/tts/tut/intro/0_6.mp3", 1000, "/tts/tut/sect1/1_1_0.mp3", 1000, "/tts/tut/sect1/1_1_1.mp3", 200, "/tts/tut/sect1/1_1_2.mp3", 200, "/tts/tut/sect1/1_1_3.mp3", 400, "/tts/tut/sect1/1_1_4.mp3", 200, "/tts/tut/sect1/1_1_5.mp3"), "plugin.keyboard.unLock");
            
            break;
        case 1:
            if (index == 0) {
                storyLine = 2;
                plugin.keyboard.key_MouseOver = function() { };
                plugin.keyboard.key_Clicked = function() { };
                plugin.keyboard.lock();
                document['black_flashplugin'].readLocal("/tts/tut/sect1/1_2_1.mp3", "TutorialStoryBoard");//very good you clicked on bbc

                plugin.keyboard.openKeyboardInput();
                lockButtons(0, "visible");
                lockButtons(1, "hidden");
                lockButtons(2, "hidden");
                lockButtons(3, "hidden");
                lockButtons(4, "hidden");

            } else {
            plugin.keyboard.lock()
                document['black_flashplugin'].readLocal("/tts/tut/sect1/1_3_1.mp3", "plugin.keyboard.unLock");//sorry not bbc
            }
            break;
        case 2:
            plugin.keyboard.openKeyboardInput();
            storyLine = 3;
            plugin.keyboard.key_Clicked = TutorialStoryBoard;
            plugin.keyboard.lock();
            plugin.keyboard.show();
            plugin.keyboard.key_MouseOver = function(button, index) { };
            document['black_flashplugin'].readLocal(new Array(1000, "/tts/tut/sect2/2_1_0.mp3", 1000, "/tts/tut/sect2/2_1_2.mp3"
            ,200,"/tts/tut/sect2/2_1_3.mp3", 400, "/tts/tut/sect2/2_1_4.mp3"
            , 200, "/tts/tut/sect2/2_1_5.mp3", 300, "/tts/tut/sect2/2_1_6.mp3"
            , 300, "/tts/tut/sect2/2_1_7.mp3", 100, "/tts/tut/sect2/2_1_8.mp3"
            , 200, "/tts/tut/sect2/2_1_9.mp3", 200, "/tts/tut/sect2/2_1_10.mp3"
            , 400, "/tts/tut/sect2/2_1_11.mp3", 200, "/tts/tut/sect2/2_1_12.mp3"
            , 400, "/tts/tut/sect2/2_1_13.mp3")
            ,"plugin.keyboard.unLock");
            plugin.keyboard.onTextInput = TutorialStoryBoard;
            
            break;
        case 3:

            if (button == "HELLO".charAt(sample.length))
                sample += button;
            else {
                sample = "";
                if (button == "HELLO".charAt(sample.length))
                    sample += button;
            }


            if (sample.length == 5) {
                storyLine = 4;
                plugin.keyboard.lock();
                plugin.keyboard.onTextInput = function() { };
                
                document['black_flashplugin'].readLocal('/tts/tut/sect2/2_2_1.mp3', 'TutorialStoryBoard');//very good you typed the word hello
                
            }

            break;
        case 4:
            storyLine = 5;
            document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect2/2_4_1.mp3', 200, '/tts/tut/sect2/2_4_2.mp3'
            , 400, '/tts/tut/sect2/2_4_3.mp3', 200, '/tts/tut/sect2/2_4_4.mp3'
            , 600, '/tts/tut/sect2/2_4_5.mp3', 400, '/tts/tut/sect2/2_4_6.mp3'
            , 400, '/tts/tut/sect2/2_4_7.mp3', 400, '/tts/tut/sect2/2_4_8.mp3'
            , 200, '/tts/tut/sect2/2_4_9.mp3', 400, '/tts/tut/sect2/2_4_10.mp3'
            , 200, '/tts/tut/sect2/2_4_11.mp3', 300, '/tts/tut/sect2/2_4_12.mp3'
            , 200, '/tts/tut/sect2/2_4_13.mp3', 300, '/tts/tut/sect2/2_4_14.mp3'
            ), 'TutorialStoryBoard');
            
            break;
        case 5:
            lockButtons(0, "hidden");
            lockButtons(1, "hidden");
            lockButtons(2, "visible");
            lockButtons(3, "hidden");
            lockButtons(4, "visible");
            plugin.keyboard.unLock();
            storyLine = 6;
            plugin.keyboard.text = "";
            sample = "";
            plugin.keyboard.onTextInput = TutorialStoryBoard;
            break;
        case 6:


            if (plugin.keyboard.text == "INTERNET IS INTERESTING") {
                storyLine = 7;
                plugin.keyboard.lock();
                
                document['black_flashplugin'].readLocal('/tts/tut/sect2/2_8_1.mp3', 'TutorialStoryBoard');//very good
                
            }
            break;

        case 7:

            plugin.keyboard.onTextInput = TutorialStoryBoard;
            plugin.keyboard.key_Clicked = function() { };
            storyLine = 8;
            plugin.keyboard.lock();
            lockButtons(0, "hidden");
            lockButtons(1, "hidden");
            lockButtons(2, "visible");
            lockButtons(3, "hidden");
            lockButtons(4, "visible");

            plugin.keyboard.text = "INTERNET IS INTERESTING";
            document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect2/2_9_1.mp3'), 'plugin.keyboard.unLock'); //remove all using backspace

            break;

        case 8:
            if (plugin.keyboard.text == "") {
                storyLine = 9;
                lockButtons(0, "hidden");
                lockButtons(1, "hidden");
                lockButtons(2, "hidden");
                lockButtons(3, "hidden");
                lockButtons(4, "visible");
                plugin.keyboard.onKeyboardSubmit = TutorialStoryBoard;
                plugin.keyboard.onTextInput = function() { };
                plugin.keyboard.lock();
                
                document['black_flashplugin'].readLocal('/tts/tut/sect2/2_10_1.mp3','TutorialStoryBoard');//very good
                

            }
            break;
        case 9:
            storyLine = 10;

            document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect2/2_11_1.mp3', 200, '/tts/tut/sect2/2_11_2.mp3'
            , 400, '/tts/tut/sect2/2_11_3.mp3'), 'plugin.keyboard.unLock');

            break;
        case 10:
            if (plugin.keyboard.text != "SPEECH") {
                plugin.keyboard.lock();
                document['black_flashplugin'].readLocal('/tts/tut/sect2/2_12_1.mp3', "plugin.keyboard.unLock");
            }
            else {
                storyLine = 11;
                plugin.keyboard.lock();
                plugin.keyboard.onKeyboardExit = TutorialStoryBoard;
                lockButtons(0, "hidden");
                lockButtons(1, "hidden");
                lockButtons(2, "hidden");
                lockButtons(3, "hidden");
                lockButtons(4, "hidden");

                document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect2/2_13_1.mp3', 400, '/tts/tut/sect2/2_13_2.mp3'
                , 200, '/tts/tut/sect2/2_13_3.mp3', 400, '/tts/tut/sect2/2_13_4.mp3'
                , 200, '/tts/tut/sect2/2_13_5.mp3'), 'plugin.keyboard.unLock');

            }
            break;
        case 11:
            storyLine = 12;
            plugin.keyboard.lock();
            plugin.keyboard.closeKeyboardInput();
            
            document['black_flashplugin'].readLocal('/tts/tut/sect2/2_14_1.mp3','TutorialStoryBoard');
            
            break;
        case 12:
            storyLine = 13;

            document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect3/3_1_0.mp3', 1000, '/tts/tut/sect3/3_1_1.mp3'
            , 200, '/tts/tut/sect3/3_1_2.mp3', 200, '/tts/tut/sect3/3_1_3.mp3'
            , 200, '/tts/tut/sect3/3_1_4.mp3', 300, '/tts/tut/sect3/3_1_5.mp3'
            , 400, '/tts/tut/sect3/3_1_6.mp3'

            , 600, '/tts/tut/sect3/3_2_1.mp3', 200, '/tts/tut/sect3/3_2_2.mp3'
            , 200, '/tts/tut/sect3/3_2_3.mp3', 200, '/tts/tut/sect3/3_2_4.mp3'
            , 400, '/tts/tut/sect3/3_2_5.mp3', 400, '/tts/tut/sect3/3_2_6.mp3'
            , 400, '/tts/tut/sect3/3_2_7.mp3', 400, '/tts/tut/sect3/3_2_8.mp3'
            , 400, '/tts/tut/sect3/3_2_9.mp3', 400, '/tts/tut/sect3/3_2_10.mp3'
            , 200, '/tts/tut/sect3/3_2_11.mp3'
            ), "TutorialStoryBoard");

            break;
        case 13:
            storyLine = 14;
            document['black_flashplugin'].readLocal(new Array(1000, '/tts/tut/sect4/4_1_0.mp3', 1000, '/tts/tut/sect4/4_1_1.mp3'
            , 400, '/tts/tut/sect4/4_1_2.mp3', 200, '/tts/tut/sect4/4_1_3.mp3'
            , 200, '/tts/tut/sect4/4_1_4.mp3', 400, '/tts/tut/sect4/4_1_5.mp3'
            , 400, '/tts/tut/sect4/4_1_6.mp3', 200, '/tts/tut/sect4/4_1_7.mp3'
            , 200, '/tts/tut/sect4/4_1_8.mp3', 400, '/tts/tut/sect4/4_1_9.mp3'
            , 200, '/tts/tut/sect4/4_1_10.mp3', 600, '/tts/tut/sect4/4_1_11.mp3'
            , 200, '/tts/tut/sect4/4_1_12.mp3',1000
            ), "initBlack")
            
            
            break;
    }

}

function playerOpen() {
    plugin.keyboard.clearKeys();
    plugin.keyboard.addKeys(player);
    plugin.keyboard.unLock();
    document.onmouseup = function() { plugin.keyboard.show(); document['black_flashplugin'].pause(); };
}

var _timer=0;

function startInitClock() {
    _timer=0;
    timer = setTimeout("startBeeping()",1000); //000);
}

function startBeeping() {
    _timer += 1;
    document['black_flashplugin'].playLocal('/sys/beep.mp3');
    if (_timer == 5) {
        clearInterval(timer);
        setTimeout("TutorialStoryBoard()", 1000);
        return;
    }
    timer = setTimeout("startBeeping()", 1000);
}