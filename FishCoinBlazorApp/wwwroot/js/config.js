/**
* Template Name: Larkon - Responsive 5 Admin Dashboard
* Author: Techzaa
* Module/App: Theme Config Js
*/

window.loadThemeConfig = function () {
    (function () {
        var savedConfig = sessionStorage.getItem("__LARKON_CONFIG__");
        var html = document.getElementsByTagName("html")[0];

        var defaultConfig = {
            theme: "light",
            topbar: { color: "light" },
            menu: { size: "sm-hover-active", color: "dark" },
        };

        config = Object.assign(JSON.parse(JSON.stringify(defaultConfig)), {});

        config.theme = html.getAttribute('data-bs-theme') || defaultConfig.theme;
        config.topbar.color = html.getAttribute('data-topbar-color') || defaultConfig.topbar.color;
        config.menu.color = html.getAttribute('data-menu-color') || defaultConfig.menu.color;
        config.menu.size = html.getAttribute('data-menu-size') || defaultConfig.menu.size;

        window.defaultConfig = JSON.parse(JSON.stringify(config));

        if (savedConfig !== null) {
            config = JSON.parse(savedConfig);
        }

        window.config = config;

        if (config) {
            html.setAttribute("data-bs-theme", config.theme);
            html.setAttribute("data-topbar-color", config.topbar.color);
            html.setAttribute("data-menu-color", config.menu.color);

            if (window.innerWidth <= 1140) {
                html.setAttribute("data-menu-size", "hidden");
            } else {
                html.setAttribute("data-menu-size", config.menu.size);
            }
        }

        // Dispatch event to notify that theme config is loaded
        document.dispatchEvent(new Event('themeConfigLoaded'));
    })();
};