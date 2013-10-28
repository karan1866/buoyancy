/*1302555209,169775555*/

if (window.CavalryLogger) { CavalryLogger.start_js(["zLeJo"]); }

var ChannelRebuildReasons={Unknown:0,AsyncError:1,TooLong:2,Refresh:3,RefreshDelay:4,UIRestart:5,NeedSeq:6,PrevFailed:7,IFrameLoadGiveUp:8,IFrameLoadRetry:9,IFrameLoadRetryWorked:10,PageTransitionRetry:11,IFrameLoadMaxSubdomain:12,ChannelUnknown:100,ChannelNoCUser:101,ChannelInvalidCUser:102,ChannelInvalidChanstr:103,ChannelChDistribTimeout:104,ChannelGetChannelOther:105,ChannelNodeShutdown:106,ChannelTermination:107,ChannelUserMismatch:108,ChannelUserMismatchShady:109,ChannelBadXs:110,ChannelSeqNeg:111,ChannelSeqTooBig:112,ChannelSeqTooSmall:113,ChannelUnexpectedJoin:114,ChannelInvalidXsCookie:115,ChannelRelocate:116,ChannelWrongPartition:117};
var CrossDocument={};(function(){CrossDocument.setListener=function(eventHandler){if(window.postMessage){if(window.addEventListener){window.addEventListener('message',eventHandler,false);}else window.onmessage=eventHandler;}else if(document.postMessage)document.addEventListener('message',eventHandler,false);};CrossDocument.mkPostMessage=function(targetWindow,targetDocument,msgHandler){if(window.postMessage){if("object"==typeof window.postMessage){return function(message,origin){targetWindow.postMessage(message,origin);};}else return bind(targetWindow,targetWindow.postMessage);}else if(document.postMessage){return bind(targetDocument,targetDocument.postMessage);}else return bind(targetWindow,msgHandler);};CrossDocument.targetOrigin=function(parent){if(window.postMessage||document.postMessage){var parentLoc=parent.location;var parentHost=parentLoc.hostname;if(parentHost=='facebook.com'||parentHost.substring(parentHost.length-13)=='.facebook.com')return parentLoc.protocol+'//'+parentLoc.host;}else return null;};var _handleMessage=function(msgCallback,msgStr){if(!msgStr||msgStr.charAt(0)!='{')return;var msg=eval('('+msgStr+')');return msgCallback(msg);};CrossDocument.mkEventHandler=function(msgCallback){return function(event){event=event||window.event;var domain=(event.domain||event.origin);if(domain.substring(domain.length-13)!='.facebook.com'&&domain.substring(domain.length-15)!='://facebook.com'&&domain!='facebook.com')return;return _handleMessage(msgCallback,event.data);};};CrossDocument.mkMessageHandler=function(msgCallback){return function(msgStr){return _handleMessage(msgCallback,msgStr);};};})();
function ChannelManager(b,f,e,a,d,c){this.user=f;this.iframeLoadMaxRetries=1;this.iframeLoadMaxSubdomain=6;this.expectResponseTimeout=5000;this.retryInterval=e;this.channelConfig=a;this._init(b,d);this.loginErrorGk=c;}ChannelManager.CONN_LOG_INTERVAL=10000;ChannelManager.prototype={_init:function(c,d){this.channelManagerId=rand32();this.config={};this.channel={};this.isActionRequest=true;this.isReady=false;this.isRebuilding=false;this.iframeIsLoaded=false;this.iframeEverLoaded=false;this.iframeCheckFailedCount=0;this.shouldClearSubdomain=false;this.subframe=c;Event.listen(this.subframe,'load',this._iframeLoadCheck.bind(this));this.postMessage=null;var a=presenceCookieManager.getSubCookie('ch');if(d){this.iframeSubdomain=null;}else{this.iframeSubdomain=0;if(a&&a.sub){for(var b=0;b<a.sub.length;b++)if(!a.sub[b]){this.iframeSubdomain=b;break;}if(b==a.sub.length)if(b==this.iframeLoadMaxSubdomain&&URI().isSecure()){this.iframeSubdomain=null;presence.error('channel: iframe max subdomains reached');this._sendDummyReconnect(ChannelRebuildReasons.IFrameLoadMaxSubdomain);}else this.iframeSubdomain=a.sub.length;}}this.handleIframeEvent=CrossDocument.mkEventHandler(this._handleIframeMessage.bind(this));this.handleIframeMessage=CrossDocument.mkMessageHandler(this._handleIframeMessage.bind(this));CrossDocument.setListener(this.handleIframeEvent.bind(this));presenceCookieManager.register('ch',this._getCookieInfo.bind(this));if(typeof window.onpageshow!='undefined'){Event.listen(window,'pagehide',this._onUnload.bind(this));Event.listen(window,'pageshow',this.rebuild.bind(this,ChannelRebuildReasons.Refresh));}else onunloadRegister(this._onUnload.bind(this));this._connTime=Env.start;this._connT=2000;this._connectivity={};(this._connSample=this._connSample.bind(this))();(this._connLog=this._connLog.bind(this))();},_connSample:function(){var a=new Date(),b=this._connState||'idle';t=a-this._connTime;this._connAdd(b,t);this._connTime=a;setTimeout(this._connSample,1000*(1+Math.random()));},_connAdd:function(a,b){this._connectivity[a]=(this._connectivity[a]||0)+b;},_connLog:function(){window.Log&&Log('channel-connectivity',this._connectivity);this._connectivity={};this._connT=Math.min(60000,2*this._connT);setTimeout(this._connLog,this._connT);},sendIframeMessage:function(b){if(!this.postMessage)return;var c=JSON.stringify(b);try{this.postMessage(c,this.targetOrigin);}catch(a){presence.error('channel: iframe msg error: '+'message "'+c+'" and error '+a.toString());}},_handleIframeMessage:function(g){var f=this.channel.currentSeq;if('seq' in g){this.channel.currentSeq=g.seq;presence.doSync();}switch(g.t){case 'init':this._connState=null;this.iframeLoaded();break;case 'log':window.chatErrorLog&&window.chatErrorLog.log(g.msg);break;case 'shutdown':if(window.loaded){presence.error('channel:refresh_'+g.reason);this.rebuild(g.reason);this.channel.shutdownHandler(true);}break;case 'connectivity':delete g.t;if('state' in g){this._connState=g.state;delete g.state;}for(var e in g)this._connAdd(e,g[e]);break;case 'fullReload':presence.error('channel:fullReload');presenceCookieManager.clear();Arbiter.inform('channel/invalid_history');break;case 'msg':var a=this.channel;var h=g.ms;var i=a.currentSeq-h.length;for(var d=0;d<h.length;d++,i++)if(i>=f){var b=h[d];try{a.msgHandler(a.name,b);}catch(c){presence.error('channel: error while handling '+b.type+' - '+c.toString());}}break;}},_onUnload:function(){this.shouldClearSubdomain=true;presenceCookieManager.setCheckUserCookie(true);presence.doSync(true);},addChannel:function(a,d,b,f,e,c){if(this.channel.name){presence.error("channel: addChannel called twice");return;}this.channel={name:a,currentSeq:d,msgHandler:b,startHandler:f,shutdownHandler:e,restartHandler:c};presence.doSync();},_getCookieInfo:function(){var b={};if(this.config.host){b.h=this.config.host;if(this.config.port)b.p=this.config.port;if(null!==this.iframeSubdomain){var a=presenceCookieManager.getSubCookie('ch');var e=(a&&a.sub)?a.sub:[];var d=e.length;if(this.shouldClearSubdomain){e[this.iframeSubdomain]=0;}else{e[this.iframeSubdomain]=1;for(var c=d;c<=this.iframeSubdomain;c++)if(!e[c])e[c]=0;}b.sub=e;}b[this.channel.name]=this.channel.currentSeq;}b.ri=this.retryInterval;return b;},getConfig:function(c,b){var a=this.channelConfig;return a&&(c in a)?a[c]:b;},stop:function(){this.stopped=true;this.setReady(false);},setReady:function(a){this.isReady=a;var b={type:'isReady',isReady:a,isActionRequest:this.isActionRequest};if(a&&this.isActionRequest)this.isActionRequest=false;if(a){b.channelName=this.channel.name;b.currentSeq=this.channel.currentSeq;b.channelManagerId=this.channelManagerId;b.channelConfig=this.channelConfig;}this.sendIframeMessage(b);},setActionRequest:function(a){this.sendIframeMessage({type:'isActionRequest',isActionRequest:a});},expectResponse:function(){this.sendIframeMessage({type:'expectResponse',newTimeout:this.expectResponseTimeout});},_iframeUrl:function(a,c,b){var d;if(null===this.iframeSubdomain){d='';}else{d=this.iframeSubdomain;d+=URI().isSecure()?'-':'.';}return new URI().setDomain(d+a+'.facebook.com').setPort(c).setPath(b).setSecure(URI().isSecure()).toString();},iframeLoad:function(a,c){this.isReady=c;this.iframeIsLoaded=false;this.config=a;var e=this._iframeUrl(a.host,a.port,a.path);clearTimeout(this._checkTimer);this._checkTimer=this._iframeCheck.bind(this).defer(this.getConfig('IFRAME_LOAD_TIMEOUT',30000));var d=null;if(!ua.ie()||ua.ie()<8)try{d=this.subframe.contentDocument;}catch(b){}if(d){try{d.location.replace(e);}catch(b){presence.error('channel: error setting location: '+b.toString());}}else if(this.subframe.contentWindow){this.subframe.src=e;}else if(this.subframe.document){this.subframe.src=e;}else presence.error('channel: error setting subframe url');presence.debug('channel: done with iframeLoad, subframe sent to '+e);},_iframeLoadCheck:function(){try{this.subframe.contentWindow.document.body.innerHTML;}catch(a){presence.error('channel:iframe load check error:(check #'+this.iframeCheckFailedCount+'):'+a);}},iframeLoaded:function(){if(!this.iframeIsLoaded){this.iframeIsLoaded=true;this.postMessage=CrossDocument.mkPostMessage(this.subframe.contentWindow,this.subframe.contentDocument,this.subframe.contentWindow.channelUplink.handleParentMessage);this.targetOrigin="*";this.setReady(this.isReady);if(this.iframeCheckFailedCount){this.channel.restartHandler(false);this._sendDummyReconnect(ChannelRebuildReasons.IFrameLoadRetryWorked);}else this.channel.startHandler();this.iframeCheckFailedCount=0;this.iframeEverLoaded=true;}},_diagnoseIframeError:function(b){function a(j){var k={},l=new Date();var h=j=='channel'?b.host:b.host2;var g=function(m){if(!k.result){k.t=new Date()-l;k.result=m?'contact':'error';e();}};k.toString=function(){return 'NAME STATUS (t=TIME)'.replace('NAME',j).replace('STATUS',k.result||'timeout').replace('TIME',Math.round(k.t/1000));};var i=new Image();i.onload=g.bind(null,true);i.onerror=g.bind(null,false);i.src=new URI().setDomain(h+'.facebook.com').setPort(b.port).setPath('/iping').setSecure(URI().isSecure());return k;}function e(g){if(g||(c.result&&d.result))presence.error('channel:iframe_failed:'+c+', '+d);}var f=e.bind(null,true).defer(120000);var c=a('channel'),d=a('alias');},_iframeCheck:function(){delete this._checkTimer;if(!this.iframeIsLoaded){this.iframeCheckFailedCount++;var a=this.config;this.config={};presenceCookieManager.store();if(this.iframeCheckFailedCount<=this.iframeLoadMaxRetries){this.rebuild(ChannelRebuildReasons.IFrameLoadRetry);}else{this.channel.shutdownHandler();this._sendDummyReconnect(ChannelRebuildReasons.IFrameLoadGiveUp);this._diagnoseIframeError(a);}}else this.retryInterval=0;},_sendDummyReconnect:function(b){var a=new AsyncRequest().setURI('/ajax/presence/reconnect.php').setData({reason:b,iframe_loaded:this.iframeEverLoaded}).setOption('suppressErrorHandlerWarning',true).setOption('retries',1).setMethod('GET').setReadOnly(true).setAllowCrossPageTransition(true);a.specifiesWriteRequiredParams()&&a.send();},_rebuildResponse:function(c){var b=c.getPayload();var a=b.user_channel;presence.debug('got rebuild response with channel '+a+', seq '+b.seq+', host '+b.host+', port '+b.port);this.channel.currentSeq=b.seq;this.isRebuilding=false;if(b.path!=this.config.path||b.host!=this.config.host){this.iframeLoad(b,true);}else this.setReady(true);presenceCookieManager.store();if(typeof chatOptions!='undefined')chatOptions.setVisibility(b.visibility);this.channel.restartHandler(true);},_retryRebuild:function(c,a){var d=this.getConfig('MAX_RETRY_INTERVAL',60000);var e=this.getConfig('MIN_RETRY_INTERVAL',10000);if(a){this.retryInterval=d;}else if(this.retryInterval==0){this.retryInterval=e;}else this.retryInterval=Math.min(d,this.retryInterval*2);var b=this.retryInterval*(.75+Math.random()*.5);presence.warn('channel: retry: manager trying again in '+(b*.001)+' secs');setTimeout(this._rebuildSend.bind(this,c),this.retryInterval);},_rebuildError:function(a,b){this.channel.shutdownHandler(true);presence.error('channel: got rebuild error: '+b.getErrorDescription());if(presence.checkMaintenanceError(b)){presence.warn('channel: manager not trying again');}else if(presence.checkLoginError(b)){if(presence.inPopoutWindow||this.loginErrorGk){this._retryRebuild(ChannelRebuildReasons.PrevFailed,true);}else presence.warn('channel: manager not trying again');}else this._retryRebuild(ChannelRebuildReasons.PrevFailed,false);},_rebuildTransportError:function(a,b){this.channel.shutdownHandler(true);presence.error('channel: got rebuild transport error: '+b.getErrorDescription());this._retryRebuild(a,false);},_rebuildSend:function(b){if(!presence.hasUserCookie(true))return;if(typeof b!='number')b=ChannelRebuildReasons.Unknown;presence.debug('channel: sending rebuild');var a=new AsyncRequest().setURI('/ajax/presence/reconnect.php').setData({reason:b,iframe_loaded:this.iframeEverLoaded}).setHandler(this._rebuildResponse.bind(this)).setErrorHandler(this._rebuildError.bind(this,b)).setTransportErrorHandler(this._rebuildTransportError.bind(this,b)).setOption('suppressErrorAlerts',true).setOption('retries',1).setMethod('GET').setReadOnly(true).setAllowCrossPageTransition(true);return a.specifiesWriteRequiredParams()&&a.send();},rebuild:function(a){presenceCookieManager.setCheckUserCookie(false);if(this.stopped)return;if(this.isRebuilding){presence.debug('channel: rebuild called, but already rebuilding');return;}this.setReady(false);this.isRebuilding=true;presence.debug('channel: rebuilding');if(a==ChannelRebuildReasons.RefreshDelay)this.retryInterval=this.channelConfig.MAX_RETRY_INTERVAL;setTimeout(this._rebuildSend.bind(this,a),this.retryInterval);}};
function TinyPresence(g,c,b,a,e,d,f){this.user=g;this.name=c;this.firstName=b;this.alias=a;this.sitevars=f;this.popoutURL=env_get('www_base')+'presence/popout.php';this.updateServerTime(e);this.pageLoadTime=this.getTime();this._init(d);}TinyPresence.prototype={cookiePollTime:2000,popoutHeartbeatTime:1000,popoutHeartbeatAllowance:4000,popoutHeartbeatFirstAllowance:15000,shutdownDelay:5000,restartDelay:3000,_init:function(a){this.stateStorers=[];this.stateLoaders=[];this._syncTimeout=null;this.windowID=rand32()+1;this.cookiePoller=null;this.heartbeat=null;this.stateUpdateTime=0;this.loaded=false;this.isShutdown=false;this.isShuttingDown=false;this.isRestarting=false;this.isPermaShutdown=false;this.shutdownTime=0;this.justPoppedOut=false;this.syncPaused=0;this.poppedOut=a;this.inPopoutWindow=a;presenceCookieManager.register('state',this._getCookieData.bind(this));Arbiter.subscribe("page_transition",this.checkRebuild.bind(this));this.load();},updateServerTime:function(a){this.timeSkew=new Date().getTime()-a;},getTime:function(){return new Date().getTime()-this.timeSkew;},debug:function(a){},warn:function(a){this.logError("13003:warning:"+a);},error:function(a){this.logError("13002:error:"+a);},logError:function(a){window.Log&&Log('realtime-error',{data:a});},load:function(){var b=presenceCookieManager.getSubCookie('state');if(!b){this.debug('presence: got null state cookie, loading with current state');this._load(this._getCookieData());return;}try{this._load(b);}catch(a){this.error('presence: got load exception: '+a.toString());this._load(this._getCookieData());}},_load:function(b){this.syncPaused++;this.stateUpdateTime=verifyNumber(b.ut);this.popoutTime=verifyNumber(b.pt);this.poppedOut=!!b.p;if(this.poppedOut){if(this.inPopoutWindow)if(!this.heartbeat)this.heartbeat=setInterval(this._popoutHeartbeat.bind(this),this.popoutHeartbeatTime);}else if(this.inPopoutWindow){if(!this.loaded){this.poppedOut=true;this.doSync();}}else this.justPoppedOut=true;if(!this.inPopoutWindow&&!this.cookiePoller)this.cookiePoller=setInterval(this._pollCookie.bind(this),this.cookiePollTime);this.state=b;for(var a=0;a<this.stateLoaders.length;a++)this.stateLoaders[a](b);this.syncPaused--;this._loaded();},_loaded:function(){this.loaded=true;},_pollCookie:function(){var e=presenceCookieManager.getSubCookie('state');if(!e)return;var d=this.popoutTime;if(e.ut>this.stateUpdateTime){this.load(e);return;}if(this.poppedOut&&!this.inPopoutWindow){var a=verifyNumber(e.pt);var b=new Date().getTime()-a;var c=this.popoutHeartbeatTime+this.popoutHeartbeatAllowance;if(this.justPoppedOut)if(a==d){c+=this.popoutHeartbeatFirstAllowance;}else this.justPoppedOut=false;this.popoutTime=a;if(b>c){this.poppedOut=false;this.doSync();}}},_popoutHeartbeat:function(){this._pollCookie();if(this.poppedOut)presenceCookieManager.store();},_getCookieData:function(){var b={p:this.poppedOut?1:0,ut:this.stateUpdateTime,pt:this.inPopoutWindow?new Date().getTime():this.popoutTime};for(var a=0;a<this.stateStorers.length;a++)b=this.stateStorers[a](b);this.state=b;return this.state;},doSync:function(a){if(this.syncPaused)return;if(a){this._doSync();}else if(!this._syncTimeout)this._syncTimeout=this._doSync.bind(this).defer();},_doSync:function(){clearTimeout(this._syncTimeout);this._syncTimeout=null;this.stateUpdateTime=new Date().getTime();presenceCookieManager.store();this._load(this.state);},pauseSync:function(){this.syncPaused++;},resumeSync:function(){this.syncPaused--;this.doSync();},handleMsg:function(a,b){this._handleMsg.bind(this,a,b).defer();},_handleMsg:function(a,b){if(typeof b=='string'){if(b=='shutdown'){this.connectionShutdown();}else if(b=='restart')if(this.isShutdown)this.restart();return;}if(this.isShutdown)return false;if(!b.type)return;Arbiter.inform(PresenceMessage.getArbiterMessageType(b.type),{sender:this,channel:a,obj:b});},checkRebuild:function(){if(this.isShutdown&&!this.isPermaShutdown)channelManager.rebuild(ChannelRebuildReasons.PageTransitionRetry);},getErrorDescription:function(a){var c=a.getError();var b=a.getErrorDescription();if(!b)b=_tx("An error occurred.");if(c==1357001)b=_tx("Your session has timed out. Please log in.");return b;},checkLoginError:function(a){var b=a.getError();if(b==1357001||b==1357004||b==1348009){this.loginShutdown();return true;}return false;},checkMaintenanceError:function(a){if(a.getError()==1356007){this.maintenanceShutdown();return true;}return false;},permaShutdown:function(){this.isPermaShutdown=true;var a=_tx("Facebook {Chat} is experiencing technical problems.",{Chat:_tx("Chat")});this.shutdown(false,a,"perma_shutdown");},loginShutdown:function(){var a=_tx("Your session has timed out. Please log in.");this.shutdown(false,a,"login_shutdown");},connectionShutdown:function(b){var a=_tx("Could not connect to Facebook {Chat} at this time.",{Chat:_tx("Chat")});this.shutdown(b,a,"connection_shutdown");},maintenanceShutdown:function(){var a=_tx("Facebook {Chat} is down for maintenance at this time.",{Chat:_tx("Chat")});this.shutdown(false,a,"maintenance_shutdown");channelManager.stop();},versionShutdown:function(){var a=_tx("Please refresh the page to get the latest version of Facebook {Chat}.",{Chat:_tx("Chat")});this.shutdown(false,a,"version_shutdown");channelManager.stop();},shutdown:function(d,c,a){this.isRestarting=false;this.isShuttingDown=true;var b=new Date().getTime();this.shutdownTime=b;if(!d){this._shutdown(c,0,a);}else setTimeout(this._shutdown.bind(this,c,b,a),this.shutdownDelay);},_shutdown:function(b,c,a){if(!this.isShuttingDown&&c==this.shutdownTime)return;if(c&&this.isShutdown)return;if(typeof b!='string'||!b)b=_tx("Facebook {Chat} is experiencing technical problems.",{Chat:_tx("Chat")});if(typeof a!='string'||!a)a="undefined";this.warn("presence:displaying_shutdown:"+a);if(this.isShutdown)return;this.logError("13001:shutdown:presence:"+a);this.isShutdown=true;Arbiter.inform(PresenceMessage.SHUTDOWN,{sender:this});},restart:function(a){this.isShuttingDown=false;this.isRestarting=true;if(!a){this._restart(0);}else this._restart.bind(this,this.shutdownTime).defer(this.restartDelay);},_restart:function(a){if(!this.isRestarting||(a&&a!=this.shutdownTime))return;this.debug("presence: restarting");this.isShutdown=false;this.load();Arbiter.inform(PresenceMessage.RESTARTED,{sender:this});},start:function(){Arbiter.inform(PresenceMessage.STARTED,{sender:this});},registerStateStorer:function(a){this.stateStorers.push(a);},registerStateLoader:function(a){this.stateLoaders.push(a);},hasUserCookie:function(a){var b=this.user==getCookie('c_user');if(!b&&a)this.permaShutdown();return b;}};
function Presence(g,c,b,a,e,d,f){this.parent.construct(this,g,c,b,a,e,d,f);}Presence.extend('TinyPresence');Presence.prototype={minWidth:100,minHeight:100,defWidth:900,defHeight:650,defX:30,defY:30,_init:function(b){if(b){this.holder=$('fbDockChat');}else this.holder=document.body;this.parent._init(b);this.popoutWidth=this.defWidth;this.popoutHeight=this.defHeight;this.popoutClicked=false;this.popinClicked=false;if(this.inPopoutWindow){Util.fallbackErrorHandler=null;onbeforeunloadRegister(this.popin.bind(this,false));onunloadRegister(this.popin.bind(this,false));}if(this.inPopoutWindow){Event.listen(window,'resize',this._windowOnResize.bind(this));Event.listen(window,'keypress',this._documentKeyPress.bind(this));}var c=ua.safari();this.isSafari2=(c&&c<500);this.isOpera=(ua.opera()>0);var a=ua.firefox();this.isFF2=(a&&a<3);this.isWindows=ua.windows();if(this.inPopoutWindow){this._windowOnResize.bind(this).defer();setTimeout(this._windowOnResize.bind(this),3000);}},_load:function(a){this.parent._load(a);if(this.poppedOut){if(!this.inPopoutWindow)CSS.addClass(this.holder,'popped_out');}else{if(this.inPopoutWindow)if(this.loaded)if(!this.popinClicked)window.close();CSS.removeClass(this.holder,'popped_out');}if(this.inPopoutWindow){this._handleResize.bind(this,0,0).defer();setTimeout(this._handleResize.bind(this,0,0),100);}this.parent._loaded();},_loaded:bagofholding,_handleMsg:function(a,b){this.parent._handleMsg(a,b);if(typeof b=='string'||!b.type)return;if(this.isShutdown)return false;if(b.type=='app_msg')if(b.event_name=='beep_event'){Bootloader.loadComponents('beeper',function(){Beeper.ensureInitialized();LiveMessageReceiver.route(b);});}else LiveMessageReceiver.route(b);},popout:function(){if(this.inPopoutWindow||this.poppedOut){this.popin(true);return;}if(this.popoutClicked)return;this.popoutClicked=true;var a=window.open(this.popoutURL,"fbChatWindow","status=0,toolbar=0,location=0,menubar=0,"+"directories=0,resizable=1,scrollbars=0,"+"width="+this.popoutWidth+",height="+this.popoutHeight+","+"left="+this.defX+",top="+this.defY);CSS.removeClass(this.holder,'popped_out');this.poppedOut=true;this.justPoppedOut=true;this.popoutTime=(new Date()).getTime();this.doSync();this.popoutClicked=false;},popin:function(a){if(typeof a=='undefined')a=true;if(this.inPopoutWindow){if(this.popinClicked)return;this.popinClicked=true;}this.poppedOut=false;this.doSync();if(this.inPopoutWindow&&a)window.close();},_windowOnResize:function(){if(!this.inPopoutWindow)return;this.contentResized={};var a=Vector2.getViewportDimensions();this._handleResize(a.x-this.virtPopoutWidth,a.y-this.virtPopoutHeight);if(this.inPopoutWindow)this.popoutHeight=a.y;},_handleResize:function(b,c){var a=this.loaded?100:10;if(this.handleResizeTimer)clearTimeout(this.handleResizeTimer);this.handleResizeTimer=setTimeout(function(){this.virtPopoutWidth+=b;this.virtPopoutHeight+=c;this.popoutWidth=Math.max(this.virtPopoutWidth,this.minWidth);this.popoutHeight=Math.max(this.virtPopoutHeight,this.minHeight);Arbiter.inform(PresenceMessage.WINDOW_RESIZED,{sender:this});},a);},_documentKeyPress:function(a){if(!this.inPopoutWindow)return;a=$E(a);var b=a?a.keyCode:-1;if(b==KEYS.ESC)Event.kill(a);},renderLink:function(b,c,a){return '<a href="'+b+'"'+(this.inPopoutWindow?' target="_blank"':'')+(a?a:'')+'>'+c+'</a>';},_shutdown:function(c,d,b){this.parent._shutdown(c,d,b);if((!this.isShuttingDown&&d===this.shutdownTime)||(d&&this.isShutdown))return;if(!this.inPopoutWindow){if(Chat.isOnline())CSS.addClass(this.holder,'presence_error');var a=$('fbChatErrorNub');TooltipLink.setTooltipText(DOM.find(a,'a.fbNubButton'),c);}else{if(this.shutdownErrorDialog)this.shutdownErrorDialog.hide();this.shutdownErrorDialog=ErrorDialog.show(_tx("Facebook Chat Error"),c);}},_restart:function(a){this.parent._restart(a);if(!this.isRestarting||(a&&a!=this.shutdownTime))return;if(!this.inPopoutWindow){CSS.removeClass(this.holder,'presence_error');}else if(this.shutdownErrorDialog)this.shutdownErrorDialog.hide();},isOnline:function(){return this.state&&this.state.vis;}};function getFirstName(c){var d=c.split(" ");var b=d[0];var a=b.length;if(typeof d[1]!='undefined'&&(a==1||(a==2&&b.indexOf('.')!=-1)||(a==3&&b.toLowerCase()=='the')))b+=' '+d[1];return b;}
function LiveMessageReceiver(a){this.eventName=a;this.subs=null;this.handler=bagofholding;this.shutdownHandler=null;this.restartHandler=null;this.registered=false;this.appId=1;}LiveMessageReceiver.prototype.setAppId=function(a){this.appId=a;return this;};LiveMessageReceiver.prototype.setHandler=function(a){this.handler=a;this._dirty();return this;};LiveMessageReceiver.prototype.setRestartHandler=function(a){this.restartHandler=a.shield();this._dirty();return this;};LiveMessageReceiver.prototype.setShutdownHandler=function(a){this.shutdownHandler=a.shield();this._dirty();return this;};LiveMessageReceiver.prototype._dirty=function(){if(this.registered){this.unregister();this.register();}};LiveMessageReceiver.prototype.register=function(){var b=function(d,c){return this.handler(c);}.bind(this);var a=PresenceMessage.getAppMessageType(this.appId,this.eventName);this.subs={};this.subs.main=Arbiter.subscribe(a,b);if(this.shutdownHandler)this.subs.shut=Arbiter.subscribe(PresenceMessage.SHUTDOWN,this.shutdownHandler);if(this.restartHandler)this.subs.restart=Arbiter.subscribe(PresenceMessage.RESTARTED,this.restartHandler);this.registered=true;return this;};LiveMessageReceiver.prototype.unregister=function(){if(!this.subs)return this;for(var a in this.subs)if(this.subs[a])Arbiter.unsubscribe(this.subs[a]);this.subs=null;this.registered=false;return this;};LiveMessageReceiver.route=function(b){var a=function(c){var d=PresenceMessage.getAppMessageType(b.app_id,b.event_name);Arbiter.inform(d,c,Arbiter.BEHAVIOR_PERSISTENT);};if(b.hasCapture){new AsyncRequest().setHandler(function(c){a(c.getPayload());}).setAllowCrossPageTransition(true).handleResponse(b.response);}else a(b.response);};
var Base64=(function(){var c='ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';function d(e){e=(e.charCodeAt(0)<<16)|(e.charCodeAt(1)<<8)|e.charCodeAt(2);return String.fromCharCode(c.charCodeAt(e>>>18),c.charCodeAt((e>>>12)&63),c.charCodeAt((e>>>6)&63),c.charCodeAt(e&63));}var a='>___?456789:;<=_______'+'\0\1\2\3\4\5\6\7\b\t\n\13\f\r\16\17\20\21\22\23\24\25\26\27\30\31'+'______\32\33\34\35\36\37 !"#$%&\'()*+,-./0123';function b(e){e=(a.charCodeAt(e.charCodeAt(0)-43)<<18)|(a.charCodeAt(e.charCodeAt(1)-43)<<12)|(a.charCodeAt(e.charCodeAt(2)-43)<<6)|a.charCodeAt(e.charCodeAt(3)-43);return String.fromCharCode(e>>>16,(e>>>8)&255,e&255);}return {encode:function(f){f=unescape(encodeURI(f));var e=(f.length+2)%3;f=(f+'\0\0'.slice(e)).replace(/[\s\S]{3}/g,d);return f.slice(0,f.length+e-2)+'=='.slice(e);},decode:function(g){g=g.replace(/[^A-Za-z0-9+\/]/g,'');var f=(g.length+3)&3,e;g=(g+'AAA'.slice(f)).replace(/..../g,b);g=g.slice(0,g.length+f-3);try{return decodeURIComponent(escape(g));}catch(e){throw new Error('Not valid UTF-8');}},encodeObject:function(e){return Base64.encode(JSON.stringify(e));},decodeObject:function(e){return JSON.parse(Base64.decode(e));},encodeNums:function(e){return String.fromCharCode.apply(String,e.map(function(f){return c.charCodeAt((f|-(f>63))&-(f>0)&63);}));}};})();
function ContextualDialog(b){var a=new Dialog();copy_properties(a,ContextualDialog.prototype);a._setFromModel(b);return a;}ContextualDialog.prototype={setContext:function(a){this._context=a;this._dirty();return this;},_buildDialogContent:function(){Bootloader.loadComponents('contextual-dialog-css',function(){CSS.addClass(this._obj,'contextual_dialog');this._content=this._frame=$N('div',{className:'contextual_dialog_content'});this._arrow=$N('div',{className:'arrow'});DOM.setContent(this._popup,[this._content,this._arrow]);}.bind(this));},_resetDialogObj:function(){if(!this._context)return;var a=Vector2.getElementPosition(this._context);var c=this._context.offsetWidth,b=this._context.offsetHeight;var d=a.x,e=a.y+b;if(c<64)d+=c/2-32;new Vector2(d,e,'document').setElementPosition(this._popup);},_renderDialog:function(a){if(window!=top)this._auto_focus=false;Dialog.prototype._renderDialog.call(this,a);}};
var PrivacyBaseValue={FACEBOOK_EMPLOYEES:112,CUSTOM:111,OPEN:100,EVERYONE:80,NETWORKS_FRIENDS_OF_FRIENDS:60,NETWORKS_FRIENDS:55,FRIENDS_OF_FRIENDS:50,ALL_FRIENDS:40,SELF:10,NOBODY:0};var PrivacyFriendsValue={EVERYONE:80,NETWORKS_FRIENDS:55,FRIENDS_OF_FRIENDS:50,ALL_FRIENDS:40,SOME_FRIENDS:30,SELF:10,NO_FRIENDS:0};var PrivacySpecialPreset={ONLY_CORP_NETWORK:200,COLLEGE_NETWORK_FRIENDS_OF_FRIENDS:201,COLLEGE_NETWORK_FRIENDS:202};var PrivacyNetworkTypes={TYPE_COLLEGE:1,TYPE_HS:2,TYPE_CORP:3,TYPE_GEO:4,TYPE_MANAGED:14,TYPE_TEST:50};var PrivacyNetworksAll=1;copy_properties(PrivacyBaseValue,PrivacySpecialPreset);function PrivacyModel(){this.value=PrivacyBaseValue.ALL_FRIENDS;this.friends=PrivacyFriendsValue.NO_FRIENDS;this.networks=[];this.objects=[];this.lists=[];this.lists_x=[];this.list_anon=0;this.ids_anon=[];this.list_x_anon=0;this.ids_x_anon=[];this.tdata={};return this;}copy_properties(PrivacyModel.prototype,{init:function(k,a,h,i,f,g,d,b,e,c,j){this.value=k;this.friends=a;this.networks=h.clone();this.objects=i.clone();this.lists=f.clone();this.lists_x=g.clone();this.list_anon=d;this.ids_anon=b.clone();this.list_x_anon=e;this.ids_x_anon=c.clone();j=j||{};copy_properties(this.tdata,j);},clone:function(){var a=new PrivacyModel();a.init(this.value,this.friends,this.networks,this.objects,this.lists,this.lists_x,this.list_anon,this.ids_anon,this.list_x_anon,this.ids_x_anon,this.tdata);return a;},getData:function(){var b=['value','friends','networks','objects','lists','lists_x','list_anon','ids_anon','list_x_anon','ids_x_anon'];var d={};for(var c=0;c<b.length;++c){var a=b[c];d[a]=this[a];}return d;}});
function BasePrivacyWidget(){}BasePrivacyWidget.mixin('Arbiter',{init:function(a,c,b){this._controllerId=a;this._root=$(a);this._options=copy_properties(b||{},c||{});this._formDataKey='privacy_data';},getData:function(){return this._model.getData();},_getPrivacyData:function(a){a=a||this._fbid;var b={};b[a]=this.getData();return b;},getRoot:function(){return this._root;},_initSelector:function(a){this._selector=a;Selector.listen(a,'select',function(b){var c=Selector.getOptionValue(b.option);this._onMenuSelect(c);}.bind(this));Event.listen(a,'click',function(){this.inform('menuActivated');}.bind(this));},_isCustomSetting:function(a){return (a==PrivacyBaseValue.CUSTOM);},_updateSelector:function(a){var b=this._model.objects;if(b&&b.length){selected_value=b[0];}else selected_value=this._model.value;Selector.setSelected(this._selector,selected_value);if(!this._isCustomSetting(selected_value))return;var c=Selector.getOption(this._selector,PrivacyBaseValue.CUSTOM+'');c.setAttribute('data-label',a||_tx("Custom"));Selector.updateSelector(this._selector);},_onPrivacyChanged:function(){this._saveFormData();this.inform('privacyChanged',this.getData());Arbiter.inform(UIPrivacyWidget.GLOBAL_PRIVACY_CHANGED_EVENT,{fbid:this._fbid,data:this.getData()});},_saveFormData:function(){var b=DOM.find(this._root,'div.UIPrivacyWidget_Form');DOM.empty(b);var a={};if(this._options.useLegacyFormData){a[this._formDataKey]=this.getData();}else a[this._formDataKey]=this._getPrivacyData();Form.createHiddenInputs(a,b);}});
function UIPrivacyWidget(){this.parent.construct(this);}copy_properties(UIPrivacyWidget,{GLOBAL_PRIVACY_CHANGED_EVENT:'UIPrivacyWidget/globalPrivacyChanged',instances:{},getInstance:function(a){return this.instances[a];}});UIPrivacyWidget.extend('BasePrivacyWidget');UIPrivacyWidget.mixin('Arbiter',{init:function(i,a,b,h,c,e,g){var f={autoSave:false,saveAsDefaultFbid:0,initialExplanation:'',useLegacyFormData:false,composerEvents:false};if(b=='0')b=0;this.parent.init(a,g,f);this._lists=c;this._networks=e;this._fbid=b;this._row=h;this._groups={};for(var d in e)this._groups[e[d].fbid]=d;UIPrivacyWidget.instances[this._controllerId]=this;this._initSelector(i);this.setData(this._row,this._options.initialExplanation,true);this._saveFormData();if(this._options.composerEvents)Arbiter.subscribe('composer/publish',this.reset.bind(this));},reset:function(){this._model=this._originalModel.clone();this._modelClone=this._originalModel.clone();this._updateSelector(this._options.initialExplanation);this._saveFormData();return this;},revert:function(){this._model=this._modelClone.clone();this._updateSelector(this._previousDescription);this._saveFormData();return this;},getValue:function(){return this._model.value;},getDefaultValue:function(){return this._originalModel.value;},isEveryonePrivacy:function(){return this._model.value==PrivacyBaseValue.EVERYONE;},dialogOpen:function(){return this._dialog&&this._dialog.getRoot();},setData:function(b,a,c){this._model=new PrivacyModel();this._model.init(b.value,b.friends,b.networks,b.objects,b.lists,b.lists_x,b.list_anon,b.ids_anon,b.list_x_anon,b.ids_x_anon,b.tdata);this._modelClone=this._model.clone();if(c)this._originalModel=this._model.clone();this._previousDescription=a;this._customModel=null;this._updateSelector(a);},setLists:function(a){this._lists=a;return this;},setNetworks:function(a){this._networks=a;return this;},_isCustomSetting:function(a){return (a==PrivacyBaseValue.CUSTOM||a==PrivacyBaseValue.SELF);},_onMenuSelect:function(b){this._modelClone=this._model.clone();var a=this._isCustomSetting(this._model.value);var c=this._isCustomSetting(b);if(a&&!c)this._customModel=this._model.clone();if(!(a&&c)){this._model.value=b;this._resetModelAuxiliaryData();}if(b==PrivacyBaseValue.CUSTOM){if(this._customModel){this._model=this._customModel.clone();}else if(this._modelClone.value!=PrivacyBaseValue.CUSTOM)this._model.friends=PrivacyFriendsValue.ALL_FRIENDS;this._showDialog();}else{if(this._groups[b]){this._model=new PrivacyModel();this._model.value=PrivacyBaseValue.CUSTOM;this._model.objects=[b];}this._onPrivacyChanged();if(this._options.autoSave)this._saveSetting();}this._updateSelector();},_showDialog:function(){if(!this._fbid){this._model.list_x_anon=0;this._model.list_anon=0;}var a={controller_id:this._controllerId,privacy_data:this.getData(),fbid:this._fbid,save_as_default_fbid:this._options.saveAsDefaultFbid};this._dialog=new Dialog().setAsync(new AsyncRequest('/ajax/privacy/privacy_widget_dialog.php').setData(a)).setModal(true).show();return false;},_resetModelAuxiliaryData:function(){if(this._model.value!=PrivacyBaseValue.CUSTOM){this._model.lists_x=this._model.lists=this._model.networks=this._model.ids_anon=this._model.ids_x_anon=[];this._model.list_x_anon=0;this._model.list_anon=0;}},_saveSetting:function(a){a=a||this._fbid;new AsyncRequest('/ajax/privacy/widget_save.php').setData({privacy_data:this._getPrivacyData(a),old_privacy_data:this._modelClone.getData(),fbid:a}).setHandler(this._handleResponse.bind(this)).setErrorHandler(this._handleError.bind(this)).send();},_handleResponse:function(b){var a=b.getPayload();this.setData(a.privacy_row,a.explanation);},_handleError:function(a){AsyncResponse.defaultErrorHandler(a);this.revert();}});