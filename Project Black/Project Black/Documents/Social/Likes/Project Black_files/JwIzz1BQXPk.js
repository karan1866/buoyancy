/*1300201570,169775813*/

if (window.CavalryLogger) { CavalryLogger.start_js(["70ubB"]); }

var FriendBrowserCheckboxController={addTypeahead:function(b,a){b.subscribe('select',this.onHubSelect.bind(this,b,a));},onHubSelect:function(f,e,event,c){if(!((event=='select')&&c.selected))return;var d=this.buildNewCheckbox(e,c.selected.text,c.selected.uid);var a=$('checkboxes_'+e);DOM.appendContent(a.firstChild,d);var b=DOM.scry(f.getElement(),'input[type="button"]');if(b&&b[0])b[0].click();this.getNew(true);},buildNewCheckbox:function(i,e,j){var a=i+'_ids_'+j;var g=i+'_ids[]';var b=$N('input',{id:a,type:'checkbox',value:j,name:g,checked:true});Event.listen(b,'click',bind(this,'getNew',false));var c=$N('td',null,b);CSS.addClass(c,'vTop hLeft');var d=$N('label',null,e);var f=$N('td',null,d);CSS.addClass(f,'vMid hLeft');var h=$N('tr');h.appendChild(c);h.appendChild(f);return h;},showMore:function(){var b=$('show_more_pager');if(CSS.hasClass(b,'async_saving'))return false;var d=this.numGetNewRequests;var a=Form.serialize($('friend_browser_form'));a.show_more=true;var c=new AsyncRequest().setURI('/ajax/growth/friend_browser/checkbox.php').setData(a).setHandler(bind(this,function(e){this.showMoreHandler(e,d);})).setStatusElement($('show_more_pager')).send();},showMoreHandler:function(c,b){if(b==this.numGetNewRequests){var a=c.payload;DOM.appendContent($('friendBrowserCheckboxContentGrid'),HTML(a.results));this.updatePagerAndExtraData(a.pager,a.extra_data);}},numGetNewRequests:0,getNew:function(c){this.numGetNewRequests++;var b=this.numGetNewRequests;CSS.addClass($('friendBrowserCheckboxResultsId'),'friendBrowserCheckboxContentOnload');CSS.show($('friendBrowsingCheckboxContentLoadingIndicator'));var a=Form.serialize($('friend_browser_form'));a.used_typeahead=c;new AsyncRequest().setURI('/ajax/growth/friend_browser/checkbox.php').setData(a).setHandler(bind(this,function(d){this.getNewHandler(d,b);})).send();},getNewHandler:function(c,b){if(b==this.numGetNewRequests){var a=c.payload;DOM.setContent($('friendBrowserCheckboxContentGrid'),HTML(a.results));CSS.removeClass($('friendBrowserCheckboxResultsId'),'friendBrowserCheckboxContentOnload');CSS.hide($('friendBrowsingCheckboxContentLoadingIndicator'));this.updatePagerAndExtraData(a.pager,a.extra_data);}},updatePagerAndExtraData:function(b,a){DOM.setContent($('friendBrowserCheckboxContentPager'),HTML(b));if(b)new OnVisible($('show_more_pager'),bind(this,'showMore'),false,1000);DOM.replace($('extra_data'),HTML(a));},makeFriendRequest:function(e,f,b,a,d){var c=Form.serialize($('friend_browser_form'));c.friend_id=f;if(b)c.countdown_complete=b;if(a)c.cancel=a;if(d)c.dialog_success=d;var g=new AsyncRequest().setURI('/ajax/growth/friend_browser/friend.php').setData(c).setRelativeTo(e).send();},queuedRequests:{},queueRequest:function(a,b){this.queuedRequests[b]=true;setTimeout(bind(this,function(){this.sendQueuedRequest(a,b);}),5000);},sendQueuedRequest:function(a,b){if(this.queuedRequests[b]){var c=DOM.find(a,'^.friendBrowserUnit');CSS.removeClass(c,'friendBrowserRequesting');CSS.addClass(c,'friendBrowserRequested');this.makeFriendRequest(a,b,true);}},cancelQueuedRequest:function(a,b){this.queuedRequests[b]=false;this.makeFriendRequest(a,b,false,true);},showConfirmationDialog:function(a,b,c,e,d){ConnectDialog.newInstance(b,c,function(){FriendBrowserCheckboxController.makeFriendRequest(a,b,false,false,true);return false;},null,false,null,0,'','',e,d).show();return false;}};
add_properties('TypeaheadBehaviors',{showLoadingIndicator:function(a){a.subscribe('activity',function(b,c){CSS.conditionClass(a.getElement(),'typeaheadLoading',c.activity);});}});

if (window.Bootloader) { Bootloader.done(["70ubB"]); }