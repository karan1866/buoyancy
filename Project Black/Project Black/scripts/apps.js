var money = new Array('', '', '');
var imoney = 0;
var hrefs;
var adder = "";
var apparr;

var alphabets = new Array('A|Ay|a', 'B||b', 'C||c', 'D||d', 'E||e', 'F||f', 'G||g', 'H||h', 'I||i', 'J||j', 'K||k', 'L||l', 'M||m', 'N||n', 'O||o', 'P||p', 'Q||q', 'R||r', 'S||s', 'T||t', 'U||u', 'V||v', 'W||w', 'X||x', 'Y||y', 'Z||z');
var numeric = new Array('0||0', '1||1', '2||2', '3||3', '4||4', '5||5', '6||6', '7||7', '8||8', '9||9');
var mathematical = new Array('0||0', '1||1', '2||2', '3||3', '4||4', '5||5', '6||6', '7||7', '8||8', '9||9', ".|decimal|dp", '+|plus|pl', '-|minus|mi', '*|Multiplied by|in', "/|Divided By|by", "s|Square Root of|sr", "^|raised to the power|po");
var calc = "";


function href(innerhtml) {
    var i;
    var href = "";
    var open = false;
    innerhtml = new String(innerhtml).replace('href="', "$");
    for (i = 0; i < innerhtml.length; i++) {
        if (innerhtml.charAt(i) == "$")
            open = true;

        if ((open == true && innerhtml.charAt(i) == '"') == true)
            break;

        if (open == true)
            href += innerhtml.charAt(i);
    }
    href = href.replace("$", "");
    return href;
}

function innerText(target) {
    var text;

    if (navigator.appName != "Microsoft Internet Explorer") {
        text = target.textContent;
    }
    else {
        var text = target.innerText;
    }
    try {
        while ((text.charAt(0) == String.fromCharCode(10) || text.charAt(0) == String.fromCharCode(32)) == true) {
            if (text.charAt(0) == String.fromCharCode(10))
                text = text.replace(String.fromCharCode(10), "");
            else
                text = text.replace(String.fromCharCode(32), "");
        }
    } catch (err) { }
    return text;
}

function appClick(button, index) {
    switch (index) {
        case 0:
            plugin.keyboard.lock();

            document.onmousemove = function(text) { };
            document['black_flashplugin'].loadURL("www.bbc.com", "news");
            break;

        case 1:                     //calculator
            calc = "";

            plugin.keyboard.lock();

            document['black_flashplugin'].readLocal("/tts/apps/math.mp3", "plugin.keyboard.unLock");

            plugin.keyboard.openKeyboardInput(mathematical);

            plugin.keyboard.onKeyboardSubmit = function(text) {

                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }


                calc += "|";
                calc = calc.replace(" + |", "");
                calc = calc.replace(" ^ |", "");
                calc = calc.replace(" - |", "");
                calc = calc.replace(" * |", "");
                calc = calc.replace(" / |", "");
                calc = calc.replace(" sr |", "");

                while (!parseInt(calc.charAt(0))) {
                    calc = calc.slice(1, calc.length);
                }

                while (calc.indexOf("|") != -1)
                    calc = calc.replace("|", "");

                while (calc.indexOf(" ") != -1)
                    calc = calc.replace(" ", "");


                while (calc.indexOf("sr") != -1)
                    calc = calc.replace("sr", " sqrt ");


                calc =  "0%2B" + escape("(" + calc + ")");


                while (calc.indexOf("+") != -1)
                    calc = calc.replace("+", "%2B");

                plugin.keyboard.lock();

                document.onmousemove = function(text) { };
                document['black_flashplugin'].loadURL("www.google.com/search?q=" + calc, "calculator");

            };

            plugin.keyboard.onKeyboardExit = initBlack;

            plugin.keyboard.onTextInput = function(text) {

                if ((!parseInt(text) && text != ".") == true) {
                    if ((text == "+" || text == "-") == true) {
                        calc += text + " ";

                        calc = calc.replace("+ +", "+");
                        calc = calc.replace("- -", "+");
                        calc = calc.replace("+ -", "-");
                        calc = calc.replace("- +", "-");
                    }
                    else if (text == "s")
                        calc += "sr ";
                    else if (parseInt(calc.charAt(calc.length - 2)) >= 0)
                        calc += text + " ";
                    else {
                        document['black_flashplugin'].readLocal("/tts/apps/nvalid.mp3");
                    }
                }
                else {
                    calc += text + " ";
                    calc = calc.replace(" . ", ".");
                    calc = calc.replace("..", ".");
                }
            };
            break;

        case 2:                 //currency conversion

            imoney = 0;

            plugin.keyboard.lock();
            document['black_flashplugin'].readLocal("/tts/apps/crnum.mp3","plugin.keyboard.unLock");
            
            plugin.keyboard.openKeyboardInput(numeric);

            plugin.keyboard.onKeyboardSubmit = function(text) {

                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }

                if (imoney == 0)
                    plugin.keyboard.closeKeyboardInput(numeric);
                money[imoney] = text;
                imoney += 1

                switch (imoney) {
                    case 1:
                        plugin.keyboard.openKeyboardInput(alphabets);
                        plugin.keyboard.lock();
                        document['black_flashplugin'].readLocal("/tts/apps/crfr.mp3.","plugin.keyboard.unLock");
                        break;
                    case 2:
                        plugin.keyboard.text = "";
                        plugin.keyboard.lock();
                        document['black_flashplugin'].readLocal("/tts/apps/crto.mp3.", "plugin.keyboard.unLock");
                        break;
                    case 3:
                        plugin.keyboard.lock();
                        while (money[0].charAt(0) == "0")
                            money[0] = money[0].replace("0", "");

                        document.onmousemove = function(text) { };
                        document['black_flashplugin'].loadURL("www.google.com/search?q=" + escape(money[0] + " " + money[1] + " to " + money[2]), "currency");

                        break;
                }
            };
            plugin.keyboard.onKeyboardExit = function() {
                switch (imoney) {
                    case 0:
                        initBlack();
                        break;
                    case 1:
                        plugin.keyboard.openKeyboardInput(numeric);
                        plugin.keyboard.lock();
                        document['black_flashplugin'].readLocal("/tts/apps/crnum.mp3.", "plugin.keyboard.unLock");
                        imoney -= 1;
                        break;
                    case 2:
                        plugin.keyboard.openKeyboardInput(alphabets);
                        plugin.keyboard.lock();
                        document['black_flashplugin'].readLocal("/tts/apps/crfr.mp3.", "plugin.keyboard.unLock");
                        imoney -= 1;
                        break;
                    case 3:
                        initBlack();
                        break;
                }
            };
            break;

        case 3:         //spell
            plugin.keyboard.openKeyboardInput();

            plugin.keyboard.onKeyboardSubmit = function(text) {
                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }

                document.onmousemove = function(text) { };
                plugin.keyboard.lock();
                document['black_flashplugin'].loadURL("www.google.com/search?q=" + escape(text), "spellCheck");
            };

            plugin.keyboard.onKeyboardExit = initBlack;
            break;

        case 4:             //time
            plugin.keyboard.openKeyboardInput();

            plugin.keyboard.lock();
            document['black_flashplugin'].readLocal("/tts/apps/city.mp3", "plugin.keyboard.unLock");

            plugin.keyboard.onKeyboardSubmit = function(text) {
                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }

                plugin.keyboard.lock();
                document.onmousemove = function() { };
                document['black_flashplugin'].loadURL("www.google.com/search?q=" + escape("time "+text), "time");
            };

            plugin.keyboard.onKeyboardExit = initBlack;
            break;

        case 5:             //weather

            plugin.keyboard.openKeyboardInput();

            plugin.keyboard.lock();
            document['black_flashplugin'].readLocal("/tts/apps/city.mp3", "plugin.keyboard.unLock");
           
            plugin.keyboard.onKeyboardSubmit = function(text) {
                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }
                
                plugin.keyboard.lock();
                document.onmousemove = function() { };
                document['black_flashplugin'].loadURL("www.google.com/search?q=" + escape(("weather " + text).toLowerCase()), "weather");
            };

            plugin.keyboard.onKeyboardExit = initBlack;
            break;

        case 6:
            plugin.keyboard.openKeyboardInput();

            plugin.keyboard.lock();
            document['black_flashplugin'].readLocal("/tts/apps/term.mp3", "plugin.keyboard.unLock");

            plugin.keyboard.onKeyboardSubmit = function(text) {
                if (text == "") {
                    plugin.keyboard.lock();
                    document['black_flashplugin'].readLocal("/tts/apps/blank.mp3", "plugin.keyboard.unLock");
                    return;
                }

                while (text.indexOf(" ") != -1)
                    text = text.replace(" ", "_");

                plugin.keyboard.lock();
                document.onmousemove = function() { };


                document['black_flashplugin'].readLocal("/sys/load.mp3");
                document['black_flashplugin'].loadURL("en.wikipedia.org/wiki/" + escape(text.charAt(0) + text.toLowerCase().slice(1, text.length)), "wiki");
            };

            plugin.keyboard.onKeyboardExit = initBlack;
            break;
            
    }
}

function wiki(content) {

    document.onmousemove = plugin.keyboard.MouseMove;
    // var htmls = adder.join();
    document.getElementById("frame").contentWindow.document.body.innerHTML = new String( content);
    //adder = "";
    var doc = document.getElementById("frame").contentWindow.document;
    var data = doc.getElementById("bodyContent");

    
    var i=0;

    var speech = new Array();

    for (i = 0; i < data.childNodes.length; i++) {

        if ((data.childNodes[i].tagName == "P" || data.childNodes[i].tagName == "H2" || data.childNodes[i].tagName == "H3")==true) {
            speech[speech.length] = innerText(data.childNodes[i]);
            speech[speech.length] = parseInt(100);
        }

    }

    speech[speech.length] = parseInt(400);
    speech[speech.length] = "Article finished.";

    document['black_flashplugin'].stop();

    callPlayer(speech, initBlack);
    
}

function wikierror() {
    document['black_flashplugin'].readLocal("/tts/apps/noent.mp3","initBlack()");
}

function news(content) {
    document.onmousemove = plugin.keyboard.MouseMove;

    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    var data = doc.getElementById("news_container");

    var i;

    apparr = new Array();

    apparr[0] = "Top story 1|Top story 1, " + innerText(data.getElementsByTagName("span")[0]);
    apparr[1] = "Top story 2|Top story 2, " + innerText(data.getElementsByTagName("span")[1]);
    apparr[2] = "Top story 3|Top story 3, " + innerText(data.getElementsByTagName("a")[2]);
    apparr[3] = "Top story 4|Top story 4, " + innerText(data.getElementsByTagName("a")[3]);
    apparr[4] = "Exit|Exit";

    hrefs = new Array();

    hrefs[0] = data.getElementsByTagName("a")[0].href.replace("http://","");
    hrefs[1] = data.getElementsByTagName("a")[1].href.replace("http://", "");
    hrefs[2] = data.getElementsByTagName("a")[2].href.replace("http://", "");
    hrefs[3] = data.getElementsByTagName("a")[3].href.replace("http://", "");

    reset();
    plugin.keyboard.clearKeys();
    plugin.keyboard.addKeys(apparr);

    document['black_flashplugin'].readLocal("/tts/apps/story.mp3","plugin.keyboard.unLock");

    plugin.keyboard.key_MouseOver = function(button, index) {
    if(index!=4)
        document['black_flashplugin'].read(apparr[index].split('|')[1]);
        else
            document['black_flashplugin'].readLocal('/tts/key/exit.mp3');
    };

    plugin.keyboard.key_Clicked = function(button, index) {
        switch (index) {
            case 4:
                initBlack();
                break;
            default:
                document.onmousemove = function() { };
                document['black_flashplugin'].loadURL((hrefs[index]), "news2");
                plugin.keyboard.lock();
        }
    };

}



function news2(content) {


    document['black_flashplugin'].stop();
    document['black_flashplugin'].playLocal("/sys/load.mp3");

    document.onmousemove = plugin.keyboard.MouseMove;
    var doc = document.getElementById("frame").contentWindow.document;
    document.getElementById("frame").contentWindow.document.body.innerHTML = content;

    var divs = doc.getElementsByTagName("div");
    var speech = new Array();

    for (i = 0; i < divs.length; i++) {
        if (divs[i].className == "story-body") {
            divs = divs[i];
            break;
        }
    }


    for (i = 0; i < divs.childNodes.length; i++) {
        if ((divs.childNodes[i].tagName == "P" || divs.childNodes[i].tagName == "SPAN") == true) {
            
            speech[speech.length] = innerText(divs.childNodes[i]).replace("+","");
            speech[speech.length] = parseInt(1);
        }
    }

    speech[speech.length] = parseInt(400);
    speech[speech.length] = "Article finished.";
    
    

    callPlayer(speech, initBlack);
}



function calculator(content) {
    document.onmousemove = plugin.keyboard.MouseMove;

    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    var data = innerText(doc.getElementById("topstuff"));


    if (data == "") {
        document['black_flashplugin'].readLocal("/tts/apps/noexp.mp3", "plugin.keyboard.onKeyboardExit");
    }
    else {
        data = innerText(doc.getElementById("topstuff").getElementsByTagName("h2")[0])

        data = data.slice(data.indexOf("=") + 1, data.length);

        document['black_flashplugin'].read(data, "plugin.keyboard.onKeyboardExit");
    }
}



function currency(content) {

    document.onmousemove = plugin.keyboard.MouseMove;
    content=content.replace("google.j.l()","");
    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    var data = innerText(doc.getElementById("topstuff"));

    document.getElementById("frame").src = "";
    if (data == "") {

        document['black_flashplugin'].readLocal("/tts/apps/nocon.mp3", "plugin.keyboard.onKeyboardExit");
    }
    else {
        data = innerText(doc.getElementById("topstuff").getElementsByTagName("h2")[0])
        data = data.replace("=", "equals");


        while (data.indexOf(String.fromCharCode(160)) != -1)
            data = data.replace(String.fromCharCode(160), ",");

        while (data.indexOf(".") != -1) {
            if (parseInt(data.charAt(data.indexOf(".") - 1)) >= 0)
                data = data.replace(".", " point ");
            else
                data = data.replace(".", " ");
        }

        document['black_flashplugin'].read(data, "plugin.keyboard.onKeyboardExit");
    }
}



function spellCheck(content) {
    content = content.replace("google.j.l()", "");
    document.onmousemove = plugin.keyboard.MouseMove;

    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    var data = innerText(doc.getElementById("ires").childNodes[1].childNodes[1]);

    if (doc.getElementById("topstuff").innerHTML == "")
        document['black_flashplugin'].readLocal("/tts/apps/spell.mp3", "plugin.keyboard.unLock");
    else {
        var spell = innerText(doc.getElementById("topstuff").getElementsByTagName("a")[0]);

        while (spell.indexOf('  ') != -1)
            spell = spell.replace("  ", " ");

        if (spell.charAt(0) == " ")
            spell = spell.replace(" ", "");

        if (spell.charAt(spell.length - 1) == " ")
            spell.slice(spell.length - 1, spell.length - 1);

        while (spell.indexOf(' ') != -1)
            spell = spell.replace(" ", "|");

        spell = spell.split('').join(' ');

        spell = spell.replace("|", "and");
        
        document['black_flashplugin'].read("Your spelling is incorrect. Correct spelling is; " + spell, "initBlack");
    }

}



function time(content) {
    content = content.replace("google.j.l()", "");
    document.onmousemove = plugin.keyboard.MouseMove;

    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    var data = innerText(doc.getElementById("ires").childNodes[1].childNodes[1]);

    if (parseInt(data.charAt(0)) >= 0) {
        document['black_flashplugin'].read(data, "initBlack");
    }
    else {
        document['black_flashplugin'].read("location not found", "initBlack");
    }
}



function weather(content) {
    content = content.replace("google.j.l()", "");
    document.onmousemove = plugin.keyboard.MouseMove;

    document.getElementById("frame").contentWindow.document.body.innerHTML = content;
    var doc = document.getElementById("frame").contentWindow.document;
    
    var data = innerText(doc.getElementById("ires").childNodes[1].childNodes[1]);


    var arr = new String(data).split("°");

    if (arr.length > 1) {
        var temp = new Array('', '', '');

        var x = 0;

        for (x = 0; x < 3; x++) {
            var i = arr[x].length - 1;

            while (parseInt(arr[x].charAt(i)) >= 0) {
                temp[x] = new String(arr[x].charAt(i)) + temp[x];
                i--;
            }
            temp[x] += " degrees";
        }
        
        document['black_flashplugin'].read("Current temperature, " + temp[0] + ". Maximum temperature, " + temp[1] + ". Minimum Temperature, " + temp[2] + " .", "initBlack");

    }
    else
        document['black_flashplugin'].read("Location not found","initBlack");

}


function callPlayer(data, exitCall) {
    reset();
    
    plugin.keyboard.clearKeys();
    
    plugin.keyboard.addKeys(player);
    plugin.keyboard.lock();
    document.getElementById("lockScreen").onclick = function() {

        document['black_flashplugin'].pause();
        plugin.keyboard.unLock();
    }

    //document['black_flashplugin'].stop();
    
    document['black_flashplugin'].read(data, "plugin.keyboard.unLock");
    document['black_flashplugin'].pause();
    document['black_flashplugin'].resume();

    plugin.keyboard.key_MouseOver = function(button, index) {
        document['black_flashplugin'].readLocal(player[index].split("|")[1]);
    }

    plugin.keyboard.key_Clicked = function(button, index) {
        switch (index) {
            case 0:
                plugin.keyboard.lock();
                document['black_flashplugin'].resume();
                break;
            case 1:
                document['black_flashplugin'].rewind();
                plugin.keyboard.lock();
                break;
            case 2:
                document['black_flashplugin'].forward();
                plugin.keyboard.lock();
                break;
            case 3:
                document['black_flashplugin'].read(data, "plugin.keyboard.unLock");
                document['black_flashplugin'].pause();
                document['black_flashplugin'].resume();
                plugin.keyboard.lock();
                break;
            case 4:
                document.getElementById("lockScreen").onclick = function() { };
                exitCall();
                break;
        }
    }
}

function neterror(err) {
    plugin.keyboard.lock();
    document['black_flashplugin'].readLocal(err, "initBlack");
}