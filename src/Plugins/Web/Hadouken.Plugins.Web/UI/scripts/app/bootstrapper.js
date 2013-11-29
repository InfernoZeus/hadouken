﻿define(
    [
        'jquery',
        'overlay',
        'eventListener',
        'pageManager',
        'pluginEngine',
        'pages/settingsPage'
    ],
    function ($, Overlay, EventListener, PageManager, PluginEngine, SettingsPage) {
        function Bootstrapper() {
            this.eventListener = new EventListener();
        }

        Bootstrapper.prototype.init = function () {
            var overlay = new Overlay('body');
            overlay.show();

            this.eventListener.subscribe('web.connected', function() {
                var pageManager = PageManager.getInstance();
                pageManager.addPage(new SettingsPage());

                var pluginEngine = PluginEngine.getInstance();
                pluginEngine.load(function () {
                    pageManager.init();
                    overlay.hide();
                });
            });

            this.eventListener.connect();
        };

        return Bootstrapper;
});