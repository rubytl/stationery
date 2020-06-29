/*****
* CONFIGURATION
*/
//Main navigation
$.navigation = $('nav > ul.nav');

$.panelIconOpened = 'icon-arrow-up';
$.panelIconClosed = 'icon-arrow-down';

//Default colours
$.brandPrimary = '#20a8d8';
$.brandSuccess = '#4dbd74';
$.brandInfo = '#63c2de';
$.brandWarning = '#f8cb00';
$.brandDanger = '#f86c6b';

$.grayDark = '#2a2c36';
$.gray = '#55595c';
$.grayLight = '#818a91';
$.grayLighter = '#d1d4d7';
$.grayLightest = '#f8f9fa';

'use strict';

/****
* MAIN NAVIGATION
*/

$(document).ready(function ($) {

    // Add class .active to current link
    $.navigation.find('a').each(function () {

        var cUrl = String(window.location).split('?')[0];

        if (cUrl.substr(cUrl.length - 1) == '#') {
            cUrl = cUrl.slice(0, -1);
        }

        if ($($(this))[0].href == cUrl) {
            $(this).addClass('active');

            $(this).parents('ul').add(this).each(function () {
                $(this).parent().addClass('open');
            });
        }
    });

    // Dropdown Menu
    $.navigation.on('click', 'a', function (e) {

        if ($.ajaxLoad) {
            e.preventDefault();
        }

        if ($(this).hasClass('nav-dropdown-toggle')) {
            $(this).parent().toggleClass('open');
            resizeBroadcast();
        }

    });

    function resizeBroadcast() {

        var timesRun = 0;
        var interval = setInterval(function () {
            timesRun += 1;
            if (timesRun === 5) {
                clearInterval(interval);
            }
            window.dispatchEvent(new Event('resize'));
        }, 62.5);
    }

    /* ---------- Main Menu Open/Close, Min/Full ---------- */
    $('.navbar-toggler').click(function () {

        if ($(this).hasClass('sidebar-toggler')) {
            $('body').toggleClass('sidebar-hidden');
            resizeBroadcast();
        }

        if ($(this).hasClass('sidebar-minimizer')) {
            $('body').toggleClass('sidebar-minimized');
            resizeBroadcast();
        }

        if ($(this).hasClass('aside-menu-toggler')) {
            $('body').toggleClass('aside-menu-hidden');
            resizeBroadcast();
        }

        if ($(this).hasClass('mobile-sidebar-toggler')) {
            $('body').toggleClass('sidebar-mobile-show');
            resizeBroadcast();
        }

    });

    $('.sidebar-close').click(function () {
        $('body').toggleClass('sidebar-opened').parent().toggleClass('sidebar-opened');
    });

    /* ---------- Disable moving to top ---------- */
    $('a[href="#"][data-top!=true]').click(function (e) {
        e.preventDefault();
    });

});

/****
* CARDS ACTIONS
*/

$(document).on('click', '.card-actions a', function (e) {
    e.preventDefault();

    if ($(this).hasClass('btn-close')) {
        $(this).parent().parent().parent().fadeOut();
    } else if ($(this).hasClass('btn-minimize')) {
        var $target = $(this).parent().parent().next('.card-block');
        if (!$(this).hasClass('collapsed')) {
            $('i', $(this)).removeClass($.panelIconOpened).addClass($.panelIconClosed);
        } else {
            $('i', $(this)).removeClass($.panelIconClosed).addClass($.panelIconOpened);
        }

    } else if ($(this).hasClass('btn-setting')) {
        $('#myModal').modal('show');
    }

});

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}

function init(url) {

    /* ---------- Tooltip ---------- */
    $('[rel="tooltip"],[data-rel="tooltip"]').tooltip({ "placement": "bottom", delay: { show: 400, hide: 200 } });

    /* ---------- Popover ---------- */
    $('[rel="popover"],[data-rel="popover"],[data-toggle="popover"]').popover();

}

//convert Hex to RGBA
function convertHex(hex, opacity) {
    hex = hex.replace('#', '');
    var r = parseInt(hex.substring(0, 2), 16);
    var g = parseInt(hex.substring(2, 4), 16);
    var b = parseInt(hex.substring(4, 6), 16);

    var result = 'rgba(' + r + ',' + g + ',' + b + ',' + opacity / 100 + ')';
    return result;
}

var options = {
    responsive: true,
    maintainAspectRatio: false,
    legend: {
        display: true
    },
    scales: {
        xAxes: [{
            type: 'time',
            distribution: 'linear',
            //ticks: {
            //    min: minDate,
            //    max: maxDate,
            //    maxRotation: 0,
            //    minRotation: 0,
            //    maxTicksLimit: 20
            //},
            scaleLabel: {
                display: true,
            },
            time: {
                displayFormats: {
                    millisecond: 'DD-MM-YYYY HH:mm:ss',
                    second: 'DD-MM-YYYY HH:mm:ss',
                    minute: 'DD-MM-YYYY HH:mm:ss',
                    hour: 'DD-MM-YYYY HH:mm:ss',
                    day: 'DD-MM-YYYY',
                    week: 'll',
                    month: 'MM-YYYY',
                    quarter: '[Q]Q-YYYY',
                    year: 'DYYY'
                },
                tooltipFormat: 'DD-MM-YYYY HH:mm:ss'
            }
        }],
        yAxes: [{
            ticks: {
                beginAtZero: true,
                maxTicksLimit: 5,
                stepSize: Math.ceil(100 / 4),
                max: 100
            }
        }]
    },
    //zoom: {
    //    enabled: true,
    //    mode: 'x',
    //    limits: {
    //        max: 10,
    //        min: 0.5
    //    }
    //},
    //pan: {
    //    enabled: true,
    //    mode: 'x'
    //}
    //elements: {
    //    point: {
    //        radius: 2,
    //        hitRadius: 10,
    //        hoverRadius: 4,
    //        hoverBorderWidth: 3,
    //    }
    //}
};

var heightOptions = {
    responsive: true,
    maintainAspectRatio: false,
    legend: {
        display: true
    },
    scales: {
        xAxes: [{
            type: 'time',
            distribution: 'linear',
            //ticks: {
            //    min: minDate,
            //    max: maxDate,
            //},
            scaleLabel: {
                display: true,
            },
            time: {
                displayFormats: {
                    millisecond: 'DD-MM-YYYY HH:mm:ss',
                    second: 'DD-MM-YYYY HH:mm:ss',
                    minute: 'DD-MM-YYYY HH:mm:ss',
                    hour: 'DD-MM-YYYY HH:mm:ss',
                    day: 'DD-MM-YYYY',
                    week: 'll',
                    month: 'MM-YYYY',
                    quarter: '[Q]Q-YYYY',
                    year: 'DYYY'
                },
                tooltipFormat: 'DD-MM-YYYY HH:mm:ss'
            }
        }],
        yAxes: [{
            ticks: {
                beginAtZero: true,
                maxTicksLimit: 5,
                stepSize: Math.ceil(2000 / 5),
                max: 2000
            }
        }]
    },
    //elements: {
    //    point: {
    //        radius: 2,
    //        hitRadius: 10,
    //        hoverRadius: 4,
    //        hoverBorderWidth: 3,
    //    }
    //}
};

var pieChartOptions = {
    responsive: true,
    legend: {
        display: false
    },
    tooltips: {
        enabled: true,
    }
}

var mapOptions = {
    styles: [
        {
            featureType: "transit.station.bus",
            stylers: [
                { visibility: "off" }
            ]
        },
        {
            featureType: "poi.business",
            elementType: "labels",
            stylers: [
                { visibility: "off" }
            ]
        },
    ]
};

function getChildNodes(parentId) {
    let nodes = [];
    var children = $('#tbSiteTree .treegrid-parent-' + parentId);
    if (children.length === 0) {
        //it is the leaf node
        if ($('#site-tree-' + parentId).children()[1].innerText === 'True') {
            nodes.push(parentId);
            return nodes;
        }
    }

    children.each(function (i, element) {
        // traverse children
        var grandChildren = getChildNodes(element.id.replace('site-tree-', ''));
        grandChildren.forEach(s => nodes.push(s));
    });

    return nodes;
}

function numberRandom(min, max) {
    return Math.round(Math.floor(Math.random() * (max - min + 1) + min));
}

function addHours(eventTime, h) {
    //return moment(eventTime).add(h, 'hours').format("DD-MM-YYYY HH:mm:ss");
    return moment(eventTime).add(h, 'hours');
}

function format(eventTime) {
    //return moment(eventTime).format("DD-MM-YYYY HH:mm:ss");
    return moment(eventTime);
}

function toogleSideBarOption() {
    $('#btnSideBar').toggle('show');
    $('#myLayout').removeClass('sidebar-hidden');
}