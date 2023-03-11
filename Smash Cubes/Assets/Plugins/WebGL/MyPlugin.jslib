 var MyPlugin = {
     IsMobile: function()
     {
         return Module.SystemInfo.mobile;
     },
     
     GiveMePlayerData: function () {
         	newInstance.SendMessage('Yandex', 'SetName', player.getName());
         	newInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
     },
     
     SaveExtern: function(date) {
         	var dateString = UTF8ToString(date);
         	var myobj = JSON.parse(dateString);
         	player.setData(myobj);
     },
     
     LoadExtern: function(){
            player.getData().then(_date => {
             	const myJSON = JSON.stringify(_date);
             	newInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
         	}).catch(() => {                          
              console.log('Fail to load data');
            });
     },
     
     SetToLeaderboard : function(value){
         	sdk.getLeaderboards()
           	.then(lb => {
             lb.setLeaderboardScore('BestScore', value);
           });
     },
 };
 
 mergeInto(LibraryManager.library, MyPlugin);