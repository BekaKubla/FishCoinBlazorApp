// Set global flag for Blazor menu initialization
window.__blazorMenuInitialized = false;

// Store original functions
window.originalToggleSmHoverMenu = window.toggleSmHoverMenu;
window.originalLoadApps = window.loadApps;

// Flags to track state
let themeConfigLoaded = false;
let menuInitialized = false;
let initializationInProgress = false;
let pendingInitialization = false;
let htmlAttributeObserver = null;

// CONFIGURATION - These values can be changed by theme toggle
let CURRENT_CONFIG = {
    'data-bs-theme': 'light',  // Default to light mode
    'data-topbar-color': 'light',
    'data-menu-color': 'dark',
    'data-menu-size': window.innerWidth <= 1140 ? 'hidden' : 'sm-hover-active'
};

// Update config when theme changes
function updateThemeConfig(theme) {
    CURRENT_CONFIG['data-bs-theme'] = theme;
    const html = document.documentElement;
    html.setAttribute('data-bs-theme', theme);
}

// Attribute preservation - allows changes but restores if removed
function preserveHtmlAttributes() {
    const html = document.documentElement;
    let restoredCount = 0;

    // Check each attribute and restore ONLY if completely missing
    Object.entries(CURRENT_CONFIG).forEach(([attr, defaultValue]) => {
        if (!html.hasAttribute(attr)) {
            html.setAttribute(attr, defaultValue);
            restoredCount++;
        }
    });

    return restoredCount;
}

// Setup mutation observer to prevent attribute removal only
function setupHtmlAttributeWatcher() {
    const html = document.documentElement;

    const observer = new MutationObserver((mutations) => {
        mutations.forEach(mutation => {
            if (mutation.type === 'attributes') {
                const attrName = mutation.attributeName;

                // Only restore if attribute was completely removed
                if (CURRENT_CONFIG.hasOwnProperty(attrName) && !html.hasAttribute(attrName)) {
                    html.setAttribute(attrName, CURRENT_CONFIG[attrName]);
                }
            }
        });
    });

    observer.observe(html, {
        attributes: true,
        attributeFilter: Object.keys(CURRENT_CONFIG)
    });

    return observer;
}

// Theme toggle handler
window.toggleTheme = function () {
    const html = document.documentElement;
    const currentTheme = html.getAttribute('data-bs-theme') || 'light';
    const newTheme = currentTheme === 'light' ? 'dark' : 'light';

    // Update the HTML attribute
    html.setAttribute('data-bs-theme', newTheme);

    // Update our config
    CURRENT_CONFIG['data-bs-theme'] = newTheme;

    // Store in sessionStorage for persistence
    const config = JSON.parse(sessionStorage.getItem('__LARKON_CONFIG__') || '{}');
    config.theme = newTheme;
    sessionStorage.setItem('__LARKON_CONFIG__', JSON.stringify(config));

    return newTheme;
};

// Initialize theme from sessionStorage or default
function initializeTheme() {
    const html = document.documentElement;

    // Check sessionStorage first
    const savedConfig = sessionStorage.getItem('__LARKON_CONFIG__');
    if (savedConfig) {
        try {
            const config = JSON.parse(savedConfig);
            if (config.theme) {
                html.setAttribute('data-bs-theme', config.theme);
                CURRENT_CONFIG['data-bs-theme'] = config.theme;
            }
        } catch (e) {
            // Use default
            html.setAttribute('data-bs-theme', 'light');
        }
    } else {
        html.setAttribute('data-bs-theme', 'light');
    }

    // Ensure other attributes have defaults
    if (!html.hasAttribute('data-topbar-color')) {
        html.setAttribute('data-topbar-color', 'light');
    }
    if (!html.hasAttribute('data-menu-color')) {
        html.setAttribute('data-menu-color', 'dark');
    }
    if (!html.hasAttribute('data-menu-size')) {
        const size = window.innerWidth <= 1140 ? 'hidden' : 'sm-hover-active';
        html.setAttribute('data-menu-size', size);
    }
}

// Wait for theme config to be loaded
function waitForThemeConfig(callback, maxAttempts = 30) {
    if (themeConfigLoaded) {
        callback();
        return;
    }

    let attempts = 0;

    function check() {
        const html = document.documentElement;
        const hasAttributes = html.hasAttribute('data-bs-theme');

        if (hasAttributes) {
            themeConfigLoaded = true;
            callback();
        } else if (attempts >= maxAttempts) {
            themeConfigLoaded = true;
            callback();
        } else {
            attempts++;
            setTimeout(check, 50);
        }
    }

    check();
}

// Initialize Blazor menu
window.initializeBlazorMenu = function (menuElement, dotNetRef) {
    if (menuInitialized && window.__blazorMenuInitialized) {
        return;
    }

    if (!themeConfigLoaded) {
        pendingInitialization = true;
        waitForThemeConfig(() => {
            if (!menuInitialized) {
                performMenuInitialization(menuElement, dotNetRef);
            }
            pendingInitialization = false;
        });
        return;
    }

    performMenuInitialization(menuElement, dotNetRef);
};

// Menu initialization logic
function performMenuInitialization(menuElement, dotNetRef) {
    if (initializationInProgress) {
        pendingInitialization = true;
        return;
    }

    initializationInProgress = true;

    if (!menuElement) {
        initializationInProgress = false;
        return;
    }

    try {
        // Initialize theme first
        initializeTheme();

        // Initialize Bootstrap components
        if (typeof bootstrap !== 'undefined') {
            menuElement.querySelectorAll('[data-bs-toggle="collapse"]').forEach(toggle => {
                if (!toggle._hasBlazorHandler) {
                    toggle.removeEventListener('click', handleBlazorCollapseClick);
                    toggle.addEventListener('click', handleBlazorCollapseClick);
                    toggle._hasBlazorHandler = true;
                }
            });
        }

        if (!menuInitialized) {
            menuInitialized = true;
            window.__blazorMenuInitialized = true;
        }

        initializationInProgress = false;
        pendingInitialization = false;

    } catch (error) {
        initializationInProgress = false;
    }
}

// Handle collapse clicks with attribute preservation
function handleBlazorCollapseClick(e) {
    e.preventDefault();

    const toggle = e.currentTarget;
    const targetId = toggle.getAttribute('href') || toggle.getAttribute('data-bs-target');

    if (targetId && targetId !== '#') {
        const targetCollapse = document.querySelector(targetId);
        if (targetCollapse) {
            try {
                const collapseInstance = bootstrap.Collapse.getInstance(targetCollapse) ||
                    new bootstrap.Collapse(targetCollapse, { toggle: false });

                if (targetCollapse.classList.contains('show')) {
                    collapseInstance.hide();
                } else {
                    collapseInstance.show();
                }

                // Preserve attributes after collapse
                setTimeout(preserveHtmlAttributes, 0);
                setTimeout(preserveHtmlAttributes, 10);
                setTimeout(preserveHtmlAttributes, 50);

            } catch (error) {
                preserveHtmlAttributes();
            }
        }
    }
}

// Override loadApps
window.loadApps = function () {
    if (typeof window.originalLoadApps === 'function') {
        window.originalLoadApps();
    }

    setTimeout(() => {
        const menuElement = document.querySelector('.main-nav');
        if (menuElement) {
            window.initializeBlazorMenu(menuElement, null);
        }
    }, 500);
};

// Set up light-dark-mode button handler
function setupThemeToggle() {
    const themeToggle = document.getElementById('light-dark-mode');
    if (themeToggle) {
        themeToggle.removeEventListener('click', handleThemeToggle);
        themeToggle.addEventListener('click', handleThemeToggle);
    }
}

function handleThemeToggle(e) {
    e.preventDefault();
    const newTheme = window.toggleTheme();

    // Also update any theme-dependent UI if needed
    const html = document.documentElement;

    // Force a small repaint to ensure all components update
    document.body.style.display = 'none';
    document.body.offsetHeight; // Force reflow
    document.body.style.display = '';
}

// Initialize everything
document.addEventListener('DOMContentLoaded', function () {
    // Initialize theme
    initializeTheme();

    // Setup watcher
    htmlAttributeObserver = setupHtmlAttributeWatcher();

    // Setup theme toggle
    setupThemeToggle();

    // Initialize menu
    setTimeout(() => {
        const menuElement = document.querySelector('.main-nav');
        if (menuElement) {
            window.initializeBlazorMenu(menuElement, null);
        }
    }, 600);
});

// Listen for theme config
document.addEventListener('themeConfigLoaded', function () {
    themeConfigLoaded = true;
    initializeTheme();

    if (!menuInitialized && !initializationInProgress) {
        setTimeout(() => {
            const menuElement = document.querySelector('.main-nav');
            if (menuElement) {
                window.initializeBlazorMenu(menuElement, null);
            }
        }, 100);
    }
});

// Periodic checks (only for missing attributes)
setInterval(preserveHtmlAttributes, 1000);

// Window focus check
window.addEventListener('focus', preserveHtmlAttributes);

// Re-setup theme toggle when new elements might be loaded
const observer = new MutationObserver(function (mutations) {
    mutations.forEach(function (mutation) {
        if (mutation.addedNodes.length) {
            setupThemeToggle();
        }
    });
});

observer.observe(document.body, {
    childList: true,
    subtree: true
});