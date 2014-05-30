window.onload = (function (exports) {

    'use strict';

    var _verbose = true;

    var pageboot = exports.pageboot = {

        start: function () {
            var requiredModules = JSON.parse(
                    document.querySelector("body")
                    .getAttribute("data-required-javascript")
                ),
                i, currentModule, contentFoundInCache,
                modulesToRequestFromServer = [];

            pageboot.moduleSet = requiredModules;

            for (i = 0; i < requiredModules.length; i++) {
                currentModule = requiredModules[i];
                contentFoundInCache = pageboot.cache.fetch(currentModule.name);
                if (contentFoundInCache) {
                    currentModule.content = contentFoundInCache;
                }
                else {
                    modulesToRequestFromServer.push(currentModule);
                }
            }

            if (modulesToRequestFromServer.length > 0) {
                pageboot.requester.sendRequest(modulesToRequestFromServer);
            }
            else {
                pageboot.bootThePage();
            }
        },

        requester: {

            sendRequest: function (dataToSend) {

                var xmlhttp = pageboot.requester.XHRInstance = new XMLHttpRequest();   // new HttpRequest instance 
                xmlhttp.open("POST", "/fetch/javascript");
                xmlhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");

                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        pageboot.requester.success(xmlhttp.responseText);
                    }
                    else {
                        // TODO: detect error
                    }
                }

                xmlhttp.send(JSON.stringify(dataToSend));
            },

            XHRInstance: null,

            success: function (rawData) {
                var serverData = JSON.parse(rawData),
                    i, resultIndex, resultContent, resultName;
                
                for (i = 0; i < serverData.length; i++) {
                    resultIndex = serverData[i].sequence;
                    resultName = serverData[i].sequence;
                    resultContent = serverData[i].content;

                    pageboot.moduleSet[resultIndex].content = resultContent;

                    pageboot.cache.save(
                        serverData[i].name,
                        resultContent
                    );
                }

                pageboot.bootThePage();
            },

            failure: function () { console.error(arguments); }

        },

        cache: {
            _keyPrefix: "pageboot_module_",
            fetch: function (moduleName) {
                return window.localStorage.getItem(this._keyPrefix + moduleName) || false;
            },
            save: function (moduleName, content) {
                try {
                    window.localStorage.setItem(
                        this._keyPrefix + moduleName,
                        content
                    );
                }
                catch (exc) {
                    // TODO: think through best fallback for private browsing situations
                    console.error(exc);
                }
            },
            clear: function () {
                for (var key in window.localStorage) {
                    if (key.indexOf(this._keyPrefix) === 0)
                        window.localStorage.removeItem(key);
                }
            }
        },

        builder: {
            combineContent: function () {
                var i, contentList = [];

                for (i = 0; i < pageboot.moduleSet.length; i++) {
                    contentList.push(pageboot.moduleSet[i].content);
                }

                return contentList.join(";");
            },

            injectCombinedToDOM: function (scriptContent) {
                var scriptEl = document.createElement("script");
                scriptEl.setAttribute("type", "text/javascript");
                scriptEl.innerHTML = scriptContent;
                document.querySelector("body").appendChild(scriptEl);
            }
        },

        bootThePage: function () {
            pageboot.builder.injectCombinedToDOM(
                pageboot.builder.combineContent()
            );
            pageboot.destroy();
        },

        destroy: function () {
            exports.pageboot = pageboot = null;
            var bootScriptEl = document.getElementById("pagebootScript");
            bootScriptEl.innerHTML = "";
            document.querySelector("body").removeChild(bootScriptEl);
        },

        moduleSet: null
    };

    return pageboot.start;

}(this));