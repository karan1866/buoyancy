<html>
<head>
    <title>Project Black</title>

<script type="text/javascript" src="scripts/apps.js"></script>

    <script type="text/javascript" src="scripts/plugin.js"></script>

    <script type="text/javascript" src="scripts/mainscript.js"></script>



    <link href="icon.ico" rel="SHORTCUT ICON">

    <style type="text/css">
        </style>
    <link type="text/css" href="style.css" rel="Stylesheet" />
    <link type="text/css" href="pluginstyle.css" rel="Stylesheet" />
</head>
<body onLoad="pageInit()">
    <div style="height: 100%; width: 100%; background: #000000; position: absolute; top: 0px;"
        id="content">

        
        <!--Plugin Code Starts Here-->
        <table id="keyboard" cellspacing="0" cellpadding="0">
            <tr id="keyContainer">
            </tr>
        </table>
        <table id="buttonBoard" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <input type="button" id="specialButton1" value="READ" onMouseOver="this.style.backgroundColor='#333333';plugin.keyboard.specialButtonMouseOver(this)"
                        onmouseout="this.style.backgroundColor='#222222'" onClick="plugin.keyboard.specialButtonMouseClick(this);document.getElementById(plugin.textInputId).focus();"/>
                </td>
                <td>
                    <input type="button" id="specialButton2" value="SUBMIT" onMouseOver="this.style.backgroundColor='#333333';plugin.keyboard.specialButtonMouseOver(this)"
                        onmouseout="this.style.backgroundColor='#222222'" onClick="plugin.keyboard.specialButtonMouseClick(this);document.getElementById(plugin.textInputId).focus();"/>
                </td>
                <td>
                    <input type="button" id="specialButton3" value="BACKSPACE" onMouseOver="this.style.backgroundColor='#333333';plugin.keyboard.specialButtonMouseOver(this)"
                        onmouseout="this.style.backgroundColor='#222222'" onClick="plugin.keyboard.specialButtonMouseClick(this);document.getElementById(plugin.textInputId).focus();"/>
                </td>
                <td>
                    <input type="button" id="specialButton4" value="EXIT" onMouseOver="this.style.backgroundColor='#333333';plugin.keyboard.specialButtonMouseOver(this)"
                        onmouseout="this.style.backgroundColor='#222222'" onClick="plugin.keyboard.specialButtonMouseClick(this);document.getElementById(plugin.textInputId).focus();"/>
                </td>
            </tr>
        </table>
        
        <div style="left:0px;" id="lockButton1" class="lockSingleButton" onMouseOver="document['black_flashplugin'].readLocal('/tts/key/disar.mp3')"></div>
        <div style="left:25%;" id="lockButton2" class="lockSingleButton" onMouseOver="document['black_flashplugin'].readLocal('/tts/key/disas.mp3')"></div>
        <div style="left:50%;" id="lockButton3" class="lockSingleButton" onMouseOver="document['black_flashplugin'].readLocal('/tts/key/disab.mp3')"></div>
        <div style="left:75%;" id="lockButton4" class="lockSingleButton" onMouseOver="document['black_flashplugin'].readLocal('/tts/key/disae.mp3')"></div>
        
        <div class="lockButton" id="lockButton" onMouseOver="document['black_flashplugin'].readLocal('/tts/key/disal.mp3')" ></div>
        
        <object id='black_flashplugin' style="visibility: visible;" classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'
            codebase='http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab'
            height='1' width='0' type="application/x-shockwave-flash">
            <param name='src' value='Black.swf' />
            <embed name='black_flashplugin' src='Black.swf' pluginspage='http://www.adobe.com/go/getflashplayer'
                height='1' width='0' />
        </object>
        <input id="keys" onBlur="this.focus();" onKeyUp="plugin.keyboard.textChange(event)" />
        <div id="lockScreen">
        </div>
        <!--Plugin Code Ends Here-->
        <div class="logo">
            <img class="imglogo" src="images/black.png" onMouseMove="ignoreEvent(event,'move')" onClick="ignoreEvent(event,'click')"/>
        </div>
    </div>
    <div style="visibility:hidden; position:absolute; left:0px; top:0px; width:100px; height:100px;"><frameset><iframe id="frame"></iframe></frameset></div>
    <div id="notebg">
    	<div class="_notebg"></div>
        <div class="note">
                <table class="noteheader">
                	<tr>
                    	<td id="td1" onMouseOver="noteEvent('1')" class="mytd" width="100" style="background:#fff">Viewer's Note</td>
                        <td id="td2" onMouseOver="noteEvent('2')" class="mytd" width="100"> Project Black</td>
                        <td id="td3" onMouseOver="noteEvent('3')" class="mytd" width="100">Team Black</td>
                        <td width="60ok"></td>
                    </tr>
                </table>
        	<div class="noteinn" id="div1" style="visibility:visible"><b>Dear Viewer,</b><br/><br/>
<b>Project Black</b> is a web application meant to assist people who are physically challenged by sight. To experience the application in the same way as the people for whom it is meant will  experience, please <u><i>close your eyes</i></u> from this point onwards, and do not peek!! :)<br/><br/>
You can move over (not click) the above tabs for miscellaneous information. Clicking anywhere on the screen will commence with the application. If you are a new user, please be sure to experience the tutorial. And remember, eyes closed... :)<br/><br/>
Enjoy the Internet!!<br/><br/>
- Team Black<br/><br/><br/>
<i>PS: Please use Mozilla Firefox or Google Chrome for proper application functioning. Internet Explorer is not supported.</i></div>
            <div class="noteinn" id="div2" style="visibility:hidden">
            	<b>Project Black</b> is a web application meant to make the internet a more accessible place for people who are physically challenged by sight. The Internet is the new age digital world which has made avaiable almost everything in the form of digital media. People from around the world can collaborate, access latest media, join social networks etc. We can't even begin to imagine how dull our life will become if this medium called Internet is taken away from us. This is precisely why Project Black came into existence, to share a small part of the joy and fun of this wonderful place called internet with people who deserve to experince more of this world.
            	<br/><br/>Project Black helps it's users to gain access to select portals on the internet, read articles, latest news etc. Throughout the journey, the user is assisted by Project Black's interactive voice guide ERICA (Extremely Realistic Interactive & Creative Android). The project stives to bring forward an unexplored world through a very interactive and user-friendly interface.
				<br/><br/>The scope of possibilities for this project are endless. In the future, the user's can be made available intersting games, access to social media, access to various other websites etc. This is why Team Black has decided to keep working on this project, implementing new and more interactive features and make this project reach and benefit it's actual users.
            	<br/><br/>Credits:
            	<ul>
            		<li>Google and Google Translate</li>
            		<li>Wikipedia</li>
            		<li>BBC News</li>
            	</ul>
            </div>
            <div class="noteinn" id="div3" style="visibility:hidden">
            Team Black currently consists of only one member, Karan Jit Singh, who has worked on this project in it's entirety.
<br/><br/>
<b>Karan Jit Singh</b>
<br/><br/>
<img src="images/karan.png"/>
<br/><br/>
Currently pursuing his studies in 11th grade. Karan, has been fascinated by the world of coding since 5th satndard. He is the founding member of Project Black, which had been made possible only because of his versatile coding skills. He is also an avid guitarist and loves playing football. Having deep interest in sciences, computers and astrophysics, he currently aims to crack the top ranking indian science examination, IIT-JEE, and pursue his higher studies.</div>
        </div>
        
    </div>
</body>
</html>
