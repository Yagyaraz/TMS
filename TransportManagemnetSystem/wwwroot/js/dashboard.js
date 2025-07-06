const { debug } = require("console");

//function PieChartsOrg() {
//    $.getJSON(GetPieChartDataUrlOrg(),
//        function (result) {
//            var myArray = $.map(result, function (e) {
//                return { name: e.name, y: e.count }
//            });

//            Highcharts.chart('pieChart', {
//                chart: {
//                    type: 'pie',
//                    options3d: {
//                        enabled: true,
//                        alpha: 45
//                    }
//                },
//                title: {
//                    text: 'समिति र जम्मा सदस्यहरु'
//                },
//                //subtitle: {
//                //	text: '3D donut in Highcharts'
//                //},
//                plotOptions: {
//                    pie: {
//                        innerSize: 100,
//                        depth: 45
//                    }
//                },
//                series: [{
//                    name: 'सदस्य जम्मा',
//                    data: myArray
//                }]
//            });

//        });
//}

function PieChartsOrg() {
    $.getJSON(GetPieChartDataUrlOrg(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });

            Highcharts.chart('pieChart', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45
                    }
                },
                title: {
                    text: 'Committee And Total Members'
                },
                //subtitle: {
                //	text: '3D donut in Highcharts'
                //},
                plotOptions: {
                    pie: {
                        innerSize: 100,
                        depth: 45
                    }
                },
                series: [{
                    name: 'Totel Member',
                    data: myArray
                }]
            });

        });
}

function PieChartsAppDisp() {
    $.getJSON(GetPieChartDataAppDisapp(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: parseFloat(e.count) }
            });
            Highcharts.chart('container', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45
                    }
                },
                title: {
                    text: 'Approved/Disapproved Vehicle'
                },
                //subtitle: {
                //	text: '3D donut in Highcharts'
                //},
                plotOptions: {
                    pie: {
                        innerSize: 100,
                        depth: 45
                    }
                },
                series: [{
                    name: 'Total',
                    data: myArray
                }]
            });
        });
}
//function PieChartsAppDisp() {
//    $.getJSON(GetPieChartDataAppDisapp(),
//        function (result) {
//            var myArray = $.map(result, function (e) {
//                return { name: e.name, y: parseFloat(e.count) }
//            });
//            Highcharts.chart('container', {
//                chart: {
//                    type: 'pie',
//                    options3d: {
//                        enabled: true,
//                        alpha: 45
//                    }
//                },
//                title: {
//                    text: 'सवारी स्वीकृत/अस्वीकृत'
//                },
//                //subtitle: {
//                //	text: '3D donut in Highcharts'
//                //},
//                plotOptions: {
//                    pie: {
//                        innerSize: 100,
//                        depth: 45
//                    }
//                },
//                series: [{
//                    name: 'जम्मा',
//                    data: myArray
//                }]
//            });
//        });
//}
function ColumnChart() {
    $.getJSON(GetColumnChart(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });
            Highcharts.chart('containers', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Committees'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    type: 'category',
                    labels: {
                        autoRotation: [-45, -90],
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Vehicles'
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: 'Total: <b>{point.y:.0f} </b>'
                },
                series: [{
                    name: 'Population',
                    colors: [
                        '#9b20d9', '#9215ac', '#861ec9', '#7a17e6', '#7010f9', '#691af3',
                        '#6225ed', '#5b30e7', '#533be1', '#4c46db', '#4551d5', '#3e5ccf',
                        '#3667c9', '#2f72c3', '#277dbd', '#1f88b7', '#1693b1', '#0a9eaa',
                        '#03c69b', '#00f194'
                    ],
                    colorByPoint: true,
                    groupPadding: 0,
                    data: myArray,
                    dataLabels: {
                        enabled: true,
                        rotation: -90,
                        color: '#FFFFFF',
                        inside: true,
                        verticalAlign: 'top',
                        format: '{point.y:.0f}', // one decimal
                        y: 10, // 10 pixels down from the top
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                        }
                    }
                }]
            });
        });
}
//function ColumnChart() {
//    $.getJSON(GetColumnChart(),
//        function (result) {
//            var myArray = $.map(result, function (e) {
//                return { name: e.name, y: e.count }
//            });
//            Highcharts.chart('containers', {
//                chart: {
//                    type: 'column'
//                },
//                title: {
//                    text: 'प्रदेश अनुसार कुल समिति'
//                },
//                subtitle: {
//                    text: ''
//                },
//                xAxis: {
//                    type: 'category',
//                    labels: {
//                        autoRotation: [-45, -90],
//                        style: {
//                            fontSize: '13px',
//                            fontFamily: 'Verdana, sans-serif'
//                        }
//                    }
//                },
//                yAxis: {
//                    min: 0,
//                    title: {
//                        text: 'कुल समिति'
//                    }
//                },
//                legend: {
//                    enabled: false
//                },
//                tooltip: {
//                    pointFormat: 'जम्मा: <b>{point.y:.0f} </b>'
//                },
//                series: [{
//                    name: 'Population',
//                    colors: [
//                        '#9b20d9', '#9215ac', '#861ec9', '#7a17e6', '#7010f9', '#691af3',
//                        '#6225ed', '#5b30e7', '#533be1', '#4c46db', '#4551d5', '#3e5ccf',
//                        '#3667c9', '#2f72c3', '#277dbd', '#1f88b7', '#1693b1', '#0a9eaa',
//                        '#03c69b', '#00f194'
//                    ],
//                    colorByPoint: true,
//                    groupPadding: 0,
//                    data: myArray,
//                    dataLabels: {
//                        enabled: true,
//                        rotation: -90,
//                        color: '#FFFFFF',
//                        inside: true,
//                        verticalAlign: 'top',
//                        format: '{point.y:.0f}', // one decimal
//                        y: 10, // 10 pixels down from the top
//                        style: {
//                            fontSize: '13px',
//                            fontFamily: 'Verdana, sans-serif'
//                        }
//                    }
//                }]
//            });
//        });
//}
//function PieChartForRoles() {
//    $.getJSON(GetPieChartForRoles(),
//        function (result) {
//            var myArray = $.map(result, function (e) {
//                return { name: e.name, y: e.count }
//            });
//            Highcharts.chart('container1', {
//                chart: {
//                    type: 'variablepie'
//                },
//                title: {
//                    text: 'जम्मा प्रयोगकर्ता',
//                    align: 'center'
//                },
//                tooltip: {
//                    headerFormat: '',
//                    pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> ' +
//                        '{point.name}</b><br/>' +
//                        'जम्मा: <b>{point.y}</b><br/>'
//                },
//                series: [{
//                    minPointSize: 10,
//                    innerSize: '20%',
//                    zMin: 0,
//                    name: 'countries',
//                    borderRadius: 5,
//                    data: myArray,
//                    colors: [
//                        '#4caefe',
//                        '#3dc3e8',
//                        '#2dd9db',
//                        '#1feeaf',
//                        '#0ff3a0',
//                        '#00e887',
//                        '#23e274'
//                    ]
//                }]
//            });
//        });
//}
//function LineChartVehicle() {
    
//    $.getJSON(GetLineChartVehicle(),
//        function (result) {
             
//            var categories = [];
//            var data = [];

//            $.map(result, function (e) {
//                categories.push(e.name);
//                data.push(e.count);
//            });

//            Highcharts.chart('container2', {
//                chart: {
//                    type: 'cylinder',
//                    options3d: {
//                        enabled: true,
//                        alpha: 15,
//                        beta: 15,
//                        depth: 50,
//                        viewDistance: 25
//                    }
//                },
//                title: {
//                    text: 'कुल सवारी डेटा'
//                },
//                subtitle: {
//                    text: ''
//                },
//                xAxis: {
//                    categories: categories,
//                    title: {
//                        text: 'सवारी प्रकार'
//                    },
//                    labels: {
//                        skew3d: true
//                    }
//                },
//                yAxis: {
//                    title: {
//                        margin: 20,
//                        text: 'कुल संख्या'
//                    },
//                    labels: {
//                        skew3d: true
//                    }
//                },
//                tooltip: {
//                    headerFormat: '<b>Vehicle: {point.x}</b><br>'
//                },
//                plotOptions: {
//                    series: {
//                        depth: 25,
//                        colorByPoint: true
//                    }
//                },
//                series: [{
//                    data: data,
//                    name: 'Total',
//                    showInLegend: false
//                }]
//            });

//        });
//}
function PieChartForRoles() {
    $.getJSON(GetPieChartForRoles(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });
            Highcharts.chart('container1', {
                chart: {
                    type: 'variablepie'
                },
                title: {
                    text: 'Total User',
                    align: 'center'
                },
                tooltip: {
                    headerFormat: '',
                    pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> ' +
                        '{point.name}</b><br/>' +
                        'Total: <b>{point.y}</b><br/>'
                },
                series: [{
                    minPointSize: 10,
                    innerSize: '20%',
                    zMin: 0,
                    name: 'countries',
                    borderRadius: 5,
                    data: myArray,
                    colors: [
                        '#4caefe',
                        '#3dc3e8',
                        '#2dd9db',
                        '#1feeaf',
                        '#0ff3a0',
                        '#00e887',
                        '#23e274'
                    ]
                }]
            });
        });
}
function LineChartVehicle() {

    $.getJSON(GetLineChartVehicle(),
        function (result) {

            var categories = [];
            var data = [];

            $.map(result, function (e) {
                categories.push(e.name);
                data.push(e.count);
            });

            Highcharts.chart('container2', {
                chart: {
                    type: 'cylinder',
                    options3d: {
                        enabled: true,
                        alpha: 15,
                        beta: 15,
                        depth: 50,
                        viewDistance: 25
                    }
                },
                title: {
                    text: 'Total Vehicle Data'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: categories,
                    title: {
                        text: 'Vehicle Type'
                    },
                    labels: {
                        skew3d: true
                    }
                },
                yAxis: {
                    title: {
                        margin: 20,
                        text: 'Total Count'
                    },
                    labels: {
                        skew3d: true
                    }
                },
                tooltip: {
                    headerFormat: '<b>Vehicle: {point.x}</b><br>'
                },
                plotOptions: {
                    series: {
                        depth: 25,
                        colorByPoint: true
                    }
                },
                series: [{
                    data: data,
                    name: 'Total',
                    showInLegend: false
                }]
            });

        });
}

function RouteLineChart() {

    $.getJSON(GetRouteLineChart(),
        function (result) {

            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });


            Highcharts.chart('container3', {
                chart: {
                    type: 'column'
                },
                title: {
                    align: 'center',
                    text: 'Route And Total Stops'
                },
                subtitle: {
                    align: 'left',
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Stops'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: ' +
                        '<b> Total Stops : {point.y:.0f}</b><br/>'
                },

                series: [
                    {
                        name: 'Routes',
                        colorByPoint: true,
                        data: myArray,
                    }
                ],
                drilldown: {
                    breadcrumbs: {
                        position: {
                            align: 'right'
                        }
                    },
                    series: [
                        {
                            name: 'Chrome',
                            id: 'Chrome',
                            data: [
                                [
                                    'v65.0',
                                    0.1
                                ],
                                [
                                    'v64.0',
                                    1.3
                                ],
                                [
                                    'v63.0',
                                    53.02
                                ],
                                [
                                    'v62.0',
                                    1.4
                                ],
                                [
                                    'v61.0',
                                    0.88
                                ],
                                [
                                    'v60.0',
                                    0.56
                                ],
                                [
                                    'v59.0',
                                    0.45
                                ],
                                [
                                    'v58.0',
                                    0.49
                                ],
                                [
                                    'v57.0',
                                    0.32
                                ],
                                [
                                    'v56.0',
                                    0.29
                                ],
                                [
                                    'v55.0',
                                    0.79
                                ],
                                [
                                    'v54.0',
                                    0.18
                                ],
                                [
                                    'v51.0',
                                    0.13
                                ],
                                [
                                    'v49.0',
                                    2.16
                                ],
                                [
                                    'v48.0',
                                    0.13
                                ],
                                [
                                    'v47.0',
                                    0.11
                                ],
                                [
                                    'v43.0',
                                    0.17
                                ],
                                [
                                    'v29.0',
                                    0.26
                                ]
                            ]
                        },
                        {
                            name: 'Firefox',
                            id: 'Firefox',
                            data: [
                                [
                                    'v58.0',
                                    1.02
                                ],
                                [
                                    'v57.0',
                                    7.36
                                ],
                                [
                                    'v56.0',
                                    0.35
                                ],
                                [
                                    'v55.0',
                                    0.11
                                ],
                                [
                                    'v54.0',
                                    0.1
                                ],
                                [
                                    'v52.0',
                                    0.95
                                ],
                                [
                                    'v51.0',
                                    0.15
                                ],
                                [
                                    'v50.0',
                                    0.1
                                ],
                                [
                                    'v48.0',
                                    0.31
                                ],
                                [
                                    'v47.0',
                                    0.12
                                ]
                            ]
                        },
                        {
                            name: 'Internet Explorer',
                            id: 'Internet Explorer',
                            data: [
                                [
                                    'v11.0',
                                    6.2
                                ],
                                [
                                    'v10.0',
                                    0.29
                                ],
                                [
                                    'v9.0',
                                    0.27
                                ],
                                [
                                    'v8.0',
                                    0.47
                                ]
                            ]
                        },
                        {
                            name: 'Safari',
                            id: 'Safari',
                            data: [
                                [
                                    'v11.0',
                                    3.39
                                ],
                                [
                                    'v10.1',
                                    0.96
                                ],
                                [
                                    'v10.0',
                                    0.36
                                ],
                                [
                                    'v9.1',
                                    0.54
                                ],
                                [
                                    'v9.0',
                                    0.13
                                ],
                                [
                                    'v5.1',
                                    0.2
                                ]
                            ]
                        },
                        {
                            name: 'Edge',
                            id: 'Edge',
                            data: [
                                [
                                    'v16',
                                    2.6
                                ],
                                [
                                    'v15',
                                    0.92
                                ],
                                [
                                    'v14',
                                    0.4
                                ],
                                [
                                    'v13',
                                    0.1
                                ]
                            ]
                        },
                        {
                            name: 'Opera',
                            id: 'Opera',
                            data: [
                                [
                                    'v50.0',
                                    0.96
                                ],
                                [
                                    'v49.0',
                                    0.82
                                ],
                                [
                                    'v12.1',
                                    0.14
                                ]
                            ]
                        }
                    ]
                }
            });

        });
}

//function RouteLineChart() {

//    $.getJSON(GetRouteLineChart(),
//        function (result) {

//            var myArray = $.map(result, function (e) {
//                return { name: e.name, y: e.count }
//            });


//            Highcharts.chart('container3', {
//                chart: {
//                    type: 'column'
//                },
//                title: {
//                    align: 'center',
//                    text: 'मार्ग र तिनीहरूको स्टपहरूको संख्या'
//                },
//                subtitle: {
//                    align: 'left',
//                    text: ''
//                },
//                accessibility: {
//                    announceNewData: {
//                        enabled: true
//                    }
//                },
//                xAxis: {
//                    type: 'category'
//                },
//                yAxis: {
//                    title: {
//                        text: 'जम्मा स्टपहरू'
//                    }

//                },
//                legend: {
//                    enabled: false
//                },
//                plotOptions: {
//                    series: {
//                        borderWidth: 0,
//                        dataLabels: {
//                            enabled: true,
//                            format: '{point.y:.0f}'
//                        }
//                    }
//                },

//                tooltip: {
//                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
//                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: ' +
//                        '<b> Total Stops : {point.y:.0f}</b><br/>'
//                },

//                series: [
//                    {
//                        name: 'Routes',
//                        colorByPoint: true,
//                        data: myArray,
//                    }
//                ],
//                drilldown: {
//                    breadcrumbs: {
//                        position: {
//                            align: 'right'
//                        }
//                    },
//                    series: [
//                        {
//                            name: 'Chrome',
//                            id: 'Chrome',
//                            data: [
//                                [
//                                    'v65.0',
//                                    0.1
//                                ],
//                                [
//                                    'v64.0',
//                                    1.3
//                                ],
//                                [
//                                    'v63.0',
//                                    53.02
//                                ],
//                                [
//                                    'v62.0',
//                                    1.4
//                                ],
//                                [
//                                    'v61.0',
//                                    0.88
//                                ],
//                                [
//                                    'v60.0',
//                                    0.56
//                                ],
//                                [
//                                    'v59.0',
//                                    0.45
//                                ],
//                                [
//                                    'v58.0',
//                                    0.49
//                                ],
//                                [
//                                    'v57.0',
//                                    0.32
//                                ],
//                                [
//                                    'v56.0',
//                                    0.29
//                                ],
//                                [
//                                    'v55.0',
//                                    0.79
//                                ],
//                                [
//                                    'v54.0',
//                                    0.18
//                                ],
//                                [
//                                    'v51.0',
//                                    0.13
//                                ],
//                                [
//                                    'v49.0',
//                                    2.16
//                                ],
//                                [
//                                    'v48.0',
//                                    0.13
//                                ],
//                                [
//                                    'v47.0',
//                                    0.11
//                                ],
//                                [
//                                    'v43.0',
//                                    0.17
//                                ],
//                                [
//                                    'v29.0',
//                                    0.26
//                                ]
//                            ]
//                        },
//                        {
//                            name: 'Firefox',
//                            id: 'Firefox',
//                            data: [
//                                [
//                                    'v58.0',
//                                    1.02
//                                ],
//                                [
//                                    'v57.0',
//                                    7.36
//                                ],
//                                [
//                                    'v56.0',
//                                    0.35
//                                ],
//                                [
//                                    'v55.0',
//                                    0.11
//                                ],
//                                [
//                                    'v54.0',
//                                    0.1
//                                ],
//                                [
//                                    'v52.0',
//                                    0.95
//                                ],
//                                [
//                                    'v51.0',
//                                    0.15
//                                ],
//                                [
//                                    'v50.0',
//                                    0.1
//                                ],
//                                [
//                                    'v48.0',
//                                    0.31
//                                ],
//                                [
//                                    'v47.0',
//                                    0.12
//                                ]
//                            ]
//                        },
//                        {
//                            name: 'Internet Explorer',
//                            id: 'Internet Explorer',
//                            data: [
//                                [
//                                    'v11.0',
//                                    6.2
//                                ],
//                                [
//                                    'v10.0',
//                                    0.29
//                                ],
//                                [
//                                    'v9.0',
//                                    0.27
//                                ],
//                                [
//                                    'v8.0',
//                                    0.47
//                                ]
//                            ]
//                        },
//                        {
//                            name: 'Safari',
//                            id: 'Safari',
//                            data: [
//                                [
//                                    'v11.0',
//                                    3.39
//                                ],
//                                [
//                                    'v10.1',
//                                    0.96
//                                ],
//                                [
//                                    'v10.0',
//                                    0.36
//                                ],
//                                [
//                                    'v9.1',
//                                    0.54
//                                ],
//                                [
//                                    'v9.0',
//                                    0.13
//                                ],
//                                [
//                                    'v5.1',
//                                    0.2
//                                ]
//                            ]
//                        },
//                        {
//                            name: 'Edge',
//                            id: 'Edge',
//                            data: [
//                                [
//                                    'v16',
//                                    2.6
//                                ],
//                                [
//                                    'v15',
//                                    0.92
//                                ],
//                                [
//                                    'v14',
//                                    0.4
//                                ],
//                                [
//                                    'v13',
//                                    0.1
//                                ]
//                            ]
//                        },
//                        {
//                            name: 'Opera',
//                            id: 'Opera',
//                            data: [
//                                [
//                                    'v50.0',
//                                    0.96
//                                ],
//                                [
//                                    'v49.0',
//                                    0.82
//                                ],
//                                [
//                                    'v12.1',
//                                    0.14
//                                ]
//                            ]
//                        }
//                    ]
//                }
//            });

//        });
//}

function GetPieDataOne() {
    $.getJSON(GetPieDataNumberOne(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });

            Highcharts.chart("container6", {
                chart: {
                    type: "pie",
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0,
                    },
                },
                title: { text: null },
                subtitle: { text: null },
                exporting: { enabled: false },
                credits: { enabled: false },
                accessibility: {
                    point: {
                        //valueSuffix: "%",
                        valueSuffix: null,
                    },
                },
                tooltip: {
                    //pointFormat: "{series.name}: <b>{point.percentage:.1f}%</b>",
                    pointFormat: "{series.name}: <b>{point.y}</b>",
                },
                legend: {
                    enabled: true,
                    align: "center",
                    verticalAlign: "bottom",
                    layout: "horizontal",
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: "pointer",
                        depth: 60,
                        dataLabels: {
                            enabled: false,
                        },
                        showInLegend: true,
                    },
                },
                series: [
                    {
                        name: "",
                        data: myArray,
                    },
                ],
                responsive: {
                    rules: [
                        {
                            condition: {
                                maxWidth: 600,
                            },
                            chartOptions: {
                                chart: {
                                    height: 450,
                                },
                                legend: {
                                    layout: "horizontal",
                                    align: "center",
                                    verticalAlign: "bottom",
                                },
                            },
                        },
                    ],
                },
            });
        });
}
function GetPieDataTwo() {
    $.getJSON(GetPieDataNumberTwo(),
        function (result) {
            var myArray = $.map(result, function (e) {
                return { name: e.name, y: e.count }
            });

            Highcharts.chart("revenueChart", {
                chart: {
                    type: "pie",
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0,
                    },
                },
                title: { text: null },
                subtitle: { text: null },
                exporting: { enabled: false },
                credits: { enabled: false },
                tooltip: {
                    //pointFormat: "{series.name}: <b>{point.percentage:.1f}%</b>",
                    pointFormat: "{series.name}: <b>{point.y}</b>",

                },
                plotOptions: {
                    pie: {
                        innerSize: 150,
                        depth: 55,
                        allowPointSelect: true,
                        cursor: "pointer",
                        dataLabels: {
                            enabled: false,
                        },
                        showInLegend: true,
                    },
                },
                legend: {
                    align: "center",
                    verticalAlign: "bottom",
                    layout: "horizontal",
                },
                series: [
                    {
                        name: "",
                        data: myArray,
                    },
                ],
                responsive: {
                    rules: [
                        {
                            condition: {
                                maxWidth: 600,
                            },
                            chartOptions: {
                                chart: {
                                    height: 450,
                                },
                                legend: {
                                    layout: "horizontal",
                                    align: "center",
                                    verticalAlign: "bottom",
                                },
                            },
                        },
                    ],
                },
            });
        });
}
function GetLineDataThree() {
    $.getJSON(GetLineDataNumberThree(),
        function (result) {
            const categories = result.map(item => item.category);
            const engineTypes = [...new Set(result.flatMap(item => Object.keys(item.engineTypeCounts)))];
            const series = engineTypes.map(type => ({
                name: type,
                data: result.map(item => item.engineTypeCounts[type] || 0)
            }));

            Highcharts.chart('container7', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Vehicle With Engine Type'
                },
                xAxis: {
                    categories: categories,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Number of Vehicles'
                    }
                },
                tooltip: {
                    shared: true,
                    valueSuffix: ' vehicles'
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: series
            });
        });
}
function GetLineDataFour() {
    debugger
    $.getJSON(GetLineDataNumberFour(),
        function (result) {
            result.forEach((route, index) => {
                const containerId = `route-container-${index}`;
                $('#routes-wrapper').append(`<div id="${containerId}"></div>`);

                Highcharts.chart(containerId, {
                    chart: {
                        type: 'line',
                        inverted: true
                    },
                    title: {
                        text: route.routeName
                    },
                    xAxis: {
                        categories: route.stops,
                        title: { text: 'Stops' }
                    },
                    yAxis: { visible: false },
                    series: [{
                        name: 'Stops',
                        data: route.stops.map((_, i) => i + 1),
                        marker: {
                            enabled: true,
                            symbol: 'circle'
                        }
                    }]
                });
            });
        });
}