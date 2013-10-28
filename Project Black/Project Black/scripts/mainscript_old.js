//Script

//Declarations
var speechurl = "http://translate.google.com/translate_tts?q=";
var apps;
var keyHTML = "<td><input type=button value=string /></td>";
var appwords;
var progress_value = -1;
var t;
var totalKeys;
var input = true;
var boardtype = "menu";
var cFunction;


var menu = new Array('about.com', 'answers.com', 'calculator web app', 'calender web app', 'google definitions', 'dictionary.com', 'ehow.com', 'google news', 'healthline.com', 'time anouncer', 'tutorial', 'google translate', 'weather anouncer', 'wilipedia.com', 'switch');

//speak
function speak(str) {
    var frame = document.getElementById('black_plugin_speech');

    if (frame != null) {
        frame.setAttribute("src", speechurl + escape(str));
    }
    else {
        alert("Black Plugin: iframe not found.");
    }
}

//progress
function progress() {
    if (progress_value != -1) {
        progress_value += 1;
        document.getElementById("progress").style.width = progress_value * 100 / 4 + "%";

        //document.getElementById("progressbar").style.visibility = "visible";
        document.getElementById("progressbar").style.left = document.body.clientWidth - 216;

        $('#progressbar').animate({ opacity: '1' }, 200);

        if (progress_value == 4) {
            $('#content').animate({ opacity: '0' }, 1000);
            clearInterval(t);
        }
    }
    else {
        clearInterval(t);
    }
}

//keyDown
function keydown(e) {
    var key;
    if (progress_value == -1) {
        progress_value = 0;

        if (navigator.appName == "Netscape") {
            key = e.keyCode;
        }
        else {
            key = event.keyCode;
        }
        
        if (key == 27) {
            t = setInterval("progress()", 1000);
        }
    }
    document.getElementById("keys").value = "";
}

//keyUp
function keyup() {
    progress_value = -1;
    clearInterval(t);
    $('#progressbar').animate({ opacity: '0' }, 300);
}

//mouseout
function mouseout() {
    speak("Your cursor has moved out of the web page.");
}

//mouseover
function mouseover() {
    speak("Your cursor has moved back into the web page.");
}

//mousemove
function mousemove(e) {

    if (size() == 1) {
        var x;
        if (navigator.appName == "Netscape") {
            x = e.clientX;
        }
        else {
            x = event.clientX;
        }

        document.getElementById("keyboard").style.left = (x / document.body.clientWidth) * (document.body.clientWidth - (75 * totalKeys));
    }
}

//clearKeys
function clearKeys() {
    document.getElementById("keycontainer").innerHTML = "";
}

//addKeys
function addKeys(keys) {
    totalKeys = keys.length;

    size();

    for (i = 0; i < keys.length; i++) {

        var td = document.createElement("td");
        var inp = document.createElement("input");
        inp.setAttribute("type", "button");

        inp.value = keys[i];

        if (keys[i].length > 1) {
            var arr = new Array();
            arr = keys[i].split('');

            var caption="";

            for (x = 0; x < arr.length; x++) {

                if (navigator.appName != "Netscape") {
                    caption += arr[x] + "\n";
                    inp.style.paddingLeft = "18px";
                }
                else
                    caption += arr[x] + "\n";
            }

            inp.setAttribute("class", "words");
            inp.value = (caption).toUpperCase();
        }

        

        inp.setAttribute("onmouseover", "buttonMouseOver(this)");
        inp.setAttribute("onmouseout", "buttonMouseOut(this)");
        inp.setAttribute("onclick", "buttonClick(this)")

        td.appendChild(inp);
        document.getElementById("keycontainer").appendChild(td);
    }
}

//toSimpleText
function toSimpleText(val) {
    var arr = new Array();
    arr = val.split("\n");

    for (i = 0; i < arr.length; i++) {
        if (arr.length > 1) {
            arr[i] = arr[i].charAt(0);
        }
    }
    
    return arr.join("");
}

//board
function board(type) {
    switch(type)
    {
        case "menu":
            {
                return menu;
            }
    }
}

//indexOf
function indexOf(str, array) {
    for (i = 0; i < array.length; i++) {
        //alert(str + "   " + array[i]);
        if (str == array[i]) {
            return i;
        }
    }
    return -1;
}

function buttonClick(target) {
    var i;

    if (target.value.length > 1) {
        //alert(toSimpleText(target.value).toLowerCase() + " " + toSimpleText(target.value).toLowerCase().length);
        var index = indexOf(toSimpleText(target.value).toLowerCase(), board(boardtype));
    }
    else {
        var index = indexOf(target.value, board(boardtype));
    }

    alert(index);

    switch (index) {
        case 9:
            {
                break;
            }
    }
}

//buttonMouseOut
function buttonMouseOver(target) {
    target.style.backgroundColor = '#3e95ff';
}

//buttonMouseOver
function buttonMouseOut(target) {
    target.style.backgroundColor = '#0475ff';
}

//size
function size() {
    if (totalKeys * 75 <= document.body.clientWidth) {
        document.getElementById("keyboard").style.width = "100%";
        document.getElementById("keycontainer").setAttribute("class","fixed");
        document.getElementById("keyboard").style.left = 0;
        
        return 0;
    }
    else {
        document.getElementById("keyboard").style.width = totalKeys * 75;
        document.getElementById("keycontainer").setAttribute("class", "normal");
        return 1;
    }
}

function frameLoad() {
    alert("hey");
    if (document.getElementById("web").contentWindow.document.innerHTML == null) {
        alert("ji");
    }
    else {
        alert("asdd");
    }
    alert("");
}

function pageInit() {
  
    
    addKeys(keyboard);
    
    document.getElementById('keys').focus();
    document.getElementById('content').innerHTML = "hold escape for 4 seconds to switch to visual mode" + document.getElementById('content').innerHTML;

    $("#progressbar").css( "left" , document.body.clientWidth - 216);

    document.getElementById("web").onload = frameLoad;

    document.onkeydown = keydown;
    document.onkeyup = keyup;
    window.onresize = size;
    document.getElementById("keyboard").onmousemove = mousemove;
}

//Script