Ext.Loader.setConfig({
 enabled: true
});
 
Ext.application({
     
 name: 'MyApp',
 appFolder: 'app',
 searchGlobal: 'string',
 congFilt: 'string',
 topicFilt: 'string',
 speakFilt: 'string',
 listGlobal: 'string',
 
 requires: [
               'MyApp.view.MainPanel', 'MyApp.view.ResultsPanel', 'MyApp.view.VideoPlayer', 
			   'MyApp.view.SettingsPanel', 'MyApp.view.HelpPanel', 'MyApp.view.LivePanel',
			   'MyApp.view.PlaylistPanel',
           ],
                
    views : ['MainPanel', 'ResultsPanel', 'VideoPlayer', 'SettingsPanel', 'HelpPanel', 'PlaylistPanel', 'LivePanel','View', 'PlayOverlay', 'AddedOverlay'],          
    controllers: ['Processes', 'Menu', 'Navigation'],
	stores: ['Videos', 'SearchRecords', 'Playlist'],
     
    launch: function() {
     console.log('Application launch');
     Ext.create('Ext.Container', {
      fullscreen: true,
      layout: 'vbox',
         items: [{
          flex: 1,
          xtype: 'View'
            }]
     });
	 		{
			Ext.Ajax.request({
				disableCaching : false,
				url : MyApp.app.SERVERurl,
				method: 'GET',
				dataType: 'json',
				
				success: function(response){
					
					var jsonData = Ext.JSON.decode(response.responseText);
					console.log(JSON.parse(response.responseText), 'SUCCESS: AQUIRE JSON');
					var resultMessage = JSON.parse(response.responseText);
                },
                failure: function (response) {
                	var jsonData = Ext.JSON.decode(response.responseText);
					console.log(JSON.parse(response.responseText), 'FAILURE: AQUIRE JSON');
                }

			});
		}
    }
     
});
