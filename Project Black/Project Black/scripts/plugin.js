function plugin() {

    function _Keyboard() {

        function MouseMove(e) {
            if (plugin.keyboard.size() == 1) {

                var x;
                if (navigator.appName == "Netscape") {
                    x = e.clientX;
                }
                else {
                    x = event.clientX;
                }
                document.getElementById(plugin.keyboardId).style.left = (x / document.body.clientWidth) * (document.body.clientWidth - (75 * plugin.keyboard.totalKeys));
            }
        }

        function specialButtonMouseOver(button) {
            switch (button.value) {
                case "READ":
                    document['black_flashplugin'].readLocal("/tts/key/readb.mp3");
                    break;
                case "BACKSPACE":
                    document['black_flashplugin'].readLocal("/tts/key/backb.mp3");
                    break;
                case "SUBMIT":
                    document['black_flashplugin'].readLocal("/tts/key/subb.mp3");
                    break;
                case "EXIT":
                    document['black_flashplugin'].readLocal("/tts/key/exitb.mp3");
                    break;
            }
        }

        function specialButtonMouseClick(button) {
            document['black_flashplugin'].playLocal("/sys/click.mp3");
            switch (button.value) {
                case "READ":
                    //this.lock();
                    if (this.text == "")
                    { document['black_flashplugin'].readLocal('/tts/tut/sect2/2_7_1.mp3'); } //document[plugin.flashPluginId].read("The text is empty.", "plugin.keyboard.unLock");
                    else {
                        var arr = this.text.split('');
                        var str = "";
                        var i
                        for (i = 0; i < arr.length; i++) {
                            switch (arr[i]) {
                                case ' ':
                                    str += "space ";
                                    break;

                                    break;
                                default:
                                    if (indexOf(new String(arr[i]), this.KEYBOARD) != -1) {
                                        var index = indexOf(new String(arr[i]), this.KEYBOARD)
                                        if (!this.KEYBOARD[index].split("|")[1])
                                            str += arr[i] + " ";
                                        else
                                            str += this.KEYBOARD[index].split("|")[1] + " ";
                                    }
                            }
                        }
                        plugin.keyboard.lock;
                        document[plugin.flashPluginId].read(new Array('Reading', 1000, str), "plugin.keyboard.unLock");
                    }
                    break;
                case "BACKSPACE":
                    if (this.text != "") {
                        this.text = trim(this.text, 0, this.text.length - 1);
                        this.onTextInput(8, this.text);
                    }
                    else
                        document['black_flashplugin'].readLocal('/tts/tut/sect2/2_7_1.mp3');
                    break;
                case "SUBMIT":
                    this.onKeyboardSubmit(this.text);
                    break;
                case "EXIT":
                    this.closeKeyboardInput();
                    this.onKeyboardExit();
                    break;
            }
        }

        function lock() {
            this.isLocked = true;
            document.getElementById(plugin.lockScreenId).style.visibility = "visible";
        }

        function unLock() {
            this.isLocked = false;
            document.getElementById(plugin.lockScreenId).style.visibility = "hidden";
        }

        function show() {
            document.getElementById(plugin.keyboardId).style.visibility = "visible";
        }

        function hide() {
            document.getElementById(plugin.keyboardId).style.visibility = "hidden";
        }

        function openKeyboardInput(arr) {
            this.closeKeyboardInput();

            this.isTyping = true;

            if (arr == undefined)
                this.KEYBOARD = new Array(' |Space|sp', 'A|Ay|a', 'B||b', 'C||c', 'D||d', 'E||e', 'F||f', 'G||g', 'H||h', 'I||i', 'J||j', 'K||k', 'L||l', 'M||m', 'N||n', 'O||o', 'P||p', 'Q||q', 'R||r', 'S||s', 'T||t', 'U||u', 'V||v', 'W||w', 'X||x', 'Y||y', 'Z||z', '0||0', '1||1', '2||2', '3||3', '4||4', '5||5', '6||6', '7||7', '8||8', '9||9');
            else
                this.KEYBOARD = arr;

            this.addKeys(this.KEYBOARD);
            document.getElementById(plugin.buttonBoardId).style.visibility = "visible";
        }

        function closeKeyboardInput() {
            this.clearKeys();
            this.isTyping = false;
            document.getElementById(plugin.buttonBoardId).style.visibility = "hidden";
            this.text = "";
        }

        function addKeys(keys) {

            if ((this.isTyping == true && this.totalKeys > 0) == true) {
                return;
            }

            if (keys.length != null) {
                if (this.totalKeys != 0) {
                    i = this.totalKeys;
                    this.totalKeys += keys.length;
                } else {
                this.totalKeys = keys.length;
                }
            }

            for (i = 0; i < keys.length; i++) {
                var td = document.createElement("td");
                var inp = document.createElement("input");
                inp.setAttribute("type", "button");

                inp.value = keys[i].split('|')[0];

                if (keys[i].split('|')[0].length > 1) {
                    var arr;
                    arr;
                    arr = keys[i].split('|')[0].split('');

                    var caption = "";

                    for (x = 0; x < arr.length; x++) {

                        if (navigator.appName != "Netscape") {
                            caption += arr[x] + "\n";
                            inp.style.paddingLeft = "18px";
                        }
                        else
                            caption += arr[x] + "\n";
                    }

                    inp.className = "words";
                    inp.value = (caption).toUpperCase();
                }

                var index = this.totalKeys - keys.length + i;

                attachEvents(inp, index);

                td.appendChild(inp);
                document.getElementById(plugin.keyContainerId).appendChild(td);
            }

            this.size();

        }

        function attachEvents(inp, index) {
            inp.onclick = function() { plugin.keyboard.buttonClick(inp, index); document['black_flashplugin'].playLocal("/sys/click.mp3")}
            inp.onmouseout = function() { plugin.keyboard.buttonMouseOut(inp, index); }
            inp.onmouseover = function() { plugin.keyboard.buttonMouseOver(inp, index); }
        }

        function clearKeys() {
            while (document.getElementById(plugin.keyContainerId).childNodes[0]) {
                document.getElementById(plugin.keyContainerId).removeChild(document.getElementById(plugin.keyContainerId).childNodes[0]);
            }
            this.totalKeys = 0;
            this.selectedIndex = null;
        }

        function size() {
            if (this.totalKeys * 75 <= document.body.clientWidth) {
                document.getElementById(plugin.keyboardId).style.width = "100%";
                document.getElementById(plugin.keyContainerId).className="fixed";
                document.getElementById(plugin.keyboardId).style.left = 0;

                return 0;
            }
            else {
                document.getElementById(plugin.keyboardId).style.width = this.totalKeys * 75;
                document.getElementById(plugin.keyContainerId).className="normal";
                return 1;
            }
        }

        function indexOf(str, array) {
            for (i = 0; i < array.length; i++) {
                if (str == array[i].split('|')[0]) {
                    return i;
                }
            }
            return -1;
        }

        function textChange(e) {

            if (this.isLocked == true)
                return;

            var keyCode;
            var key;
            if (navigator.appName == "Netscape") {
                keyCode = e.keyCode;
            }
            else {
                keyCode = event.keyCode;
            }

            key = String.fromCharCode(keyCode);


            if (this.isTyping == true) {
                if (indexOf(key, this.KEYBOARD) != -1) {

                    this.text += key.toUpperCase();
                    var target = document.getElementById(plugin.keyContainerId).childNodes[indexOf(key, this.KEYBOARD)].childNodes[0];

                    this.buttonMouseOver(target, indexOf(key, this.KEYBOARD));
                    this.onTextInput(key.toUpperCase(), this.text);
                    document['black_flashplugin'].playLocal('/sys/click.mp3');

                }
            }

            if (this.totalKeys > 0) {
                if ((keyCode == 39 || keyCode == 40) == true) {

                    if (this.selectedIndex == null) {
                        var target = document.getElementById(plugin.keyContainerId).childNodes[0].childNodes[0];
                        this.buttonMouseOver(target, 0);
                    }
                    else if (this.totalKeys > this.selectedIndex + 1) {
                        var target = document.getElementById(plugin.keyContainerId).childNodes[this.selectedIndex + 1].childNodes[0];

                        var x = document.getElementById(plugin.keyContainerId).childNodes[this.selectedIndex].childNodes[0];
                        this.buttonMouseOut(x, this.selectedIndex);

                        this.buttonMouseOver(target, this.selectedIndex + 1);
                    }
                    else {
                        //document['black_flashplugin'].read('end of keyboard');
                    }

                }
                else if ((keyCode == 37 || keyCode == 38) == true) {

                    if (this.selectedIndex == null) {
                        var target = document.getElementById(plugin.keyContainerId).childNodes[0].childNodes[0];
                        this.buttonMouseOver(target, this.totalKeys - 1);
                    }
                    else if (this.selectedIndex > 0) {
                        var target = document.getElementById(plugin.keyContainerId).childNodes[this.selectedIndex - 1].childNodes[0];

                        var x = document.getElementById(plugin.keyContainerId).childNodes[this.selectedIndex].childNodes[0];
                        this.buttonMouseOut(x, this.selectedIndex);

                        this.buttonMouseOver(target, this.selectedIndex - 1);
                    }
                    else {
                        //document['black_flashplugin'].read('end of keyboard');
                    }

                }
                else if (keyCode == 13) {
                    if ((this.selectedIndex>=0)==false)
                        return;

                    var target = document.getElementById(plugin.keyContainerId).childNodes[this.selectedIndex].childNodes[0];
                    this.buttonClick(target, this.selelctedIndex);
                    document['black_flashplugin'].playLocal('/sys/click.mp3');
                    
                }
            }


        }

        function trim(str, start, length) {
            var i;
            var str2 = "";
            for (i = start; i < start + length; i++) {
                str2 += str.charAt(i);
            }
            return str2;
        }

        this.buttonClick = function(button, index) {
            document.getElementById(plugin.textInputId).focus();
            if (this.isTyping == false) {
                this.key_Clicked(button, this.selectedIndex);
            } else {
                this.text += this.KEYBOARD[this.selectedIndex].split('|')[0];
                this.onTextInput(this.KEYBOARD[this.selectedIndex].split('|')[0], this.text);
            }
        };

        this.buttonMouseOver = function(target, index) {
            if (this.isLocked == true) {
                document.getElementById(plugin.textInputId).focus();
                return;
            }

            //if (this.isTyping == true) {
            if (this.selectedIndex > 0) {
                var i;
                for (i = 0; i < this.totalKeys; i++) {
                    var x = document.getElementById(plugin.keyContainerId).childNodes[i].childNodes[0];
                    this.buttonMouseOut(x, i);
                }
            }
            //}

            this.selectedIndex = index;

            target.style.backgroundColor = '#3e95ff';
            if (this.isTyping == false)
                this.key_MouseOver(target, index);
            else {

                document['black_flashplugin'].readLocal("/tts/vkey/" + this.KEYBOARD[index].split('|')[2] + ".mp3");

            }
            document.getElementById(plugin.textInputId).focus();
        }

        this.buttonMouseOut = function(target, index) {
            target.style.backgroundColor = '#0475ff';
            if (this.isTyping == false)
                this.key_MouseOut(target, index);
            else { }
            document.getElementById(plugin.textInputId).focus();
        }

        this.specialButtonMouseClick = specialButtonMouseClick;
        this.specialButtonMouseOver = specialButtonMouseOver;
        this.MouseMove = MouseMove;
        this.size = size;
        this.textChange = textChange;

        //Properties
        this.text = "";                                     //the text typed in the cirtual input keyboard (this is only when the input keyboard is open)
        this.KEYBOARD = new Array(' |Space|sp', 'A|Ay|a', 'B||b', 'C||c', 'D||d', 'E||e', 'F||f', 'G||g', 'H||h', 'I||i', 'J||j', 'K||k', 'L||l', 'M||m', 'N||n', 'O||o', 'P||p', 'Q||q', 'R||r', 'S||s', 'T||t', 'U||u', 'V||v', 'W||w', 'X||x', 'Y||y', 'Z||z', '0||0', '1||1', '2||2', '3||3', '4||4', '5||5', '6||6', '7||7', '8||8', '9||9');
        this.totalKeys = 0;
        this.isTyping = false;
        this.isLocked = false;
        this.selectedIndex;

        //Methods
        this.show = show;                                   //show keyboard
        this.hide = hide;                                   //hide keyboard
        this.lock = lock;                                   //lock keyboard (puts a transparent gray screen in front of the keyboard)
        this.unLock = unLock;                               //unlock keyboard
        this.addKeys = addKeys;                             //add keys to virtual keyboard
        this.clearKeys = clearKeys;                         //remove all keys in the virtual keyboard
        this.openKeyboardInput = openKeyboardInput;         //Close Virtual Input Keyboard
        this.closeKeyboardInput = closeKeyboardInput;       //Open Virtual Input Keyboard

        //Events
        this.onKeyboardSubmit = function(text) { };         //when submit button is clicked
        this.onKeyboardExit = function() { };               //when exit button is clickedss
        this.key_MouseOver = function(button, index) { };   //When mouse moves over a key
        this.key_MouseOut = function(button, index) { };    //When mouse moves out of a key
        this.key_Clicked = function(button, index) { };     //When a key is clicked
        this.onTextInput = function(key, value) { };        //When virtual keyboard key is entered
    }

    function load() {
        this.loadState = 1;

        this.keyboard = new _Keyboard();

        document.onmousemove = plugin.keyboard.MouseMove;
        document.getElementById(plugin.keyboardId).setAttribute("onmousemove", "plugin.keyboard.MouseMove(event)");
        document.getElementById(plugin.textInputId).focus();

        this.onLoad();
    }


    //the following is not for the developer
    this.load = load;
    this.keyboard = null;

    //Events
    this.onLoad = function() { };           //When the whole plugin is loaded

    //Properties
    this.loadState = 0;                     //if ==0 that means the plugin has not loaded yet, if ==1 that means the plugin has already loaded
    this.keyboardId = new String("keyboard");
    this.lockScreenId = new String("lockScreen");
    this.textInputId = new String("keys");
    this.buttonBoardId = new String("buttonBoard");
    this.keyContainerId = new String("keyContainer");
    this.flashPluginId = new String("black_flashplugin");

}

var plugin = new plugin();
