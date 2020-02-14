var config = {
    type: 'bar',
    data : {
    labels: ['95/05/09','95/05/10','95/05/11','95/05/12','95/05/13','95/05/14','95/05/15','95/05/16','95/05/17','95/05/18','95/05/19','95/05/20','95/05/21','95/05/22','95/05/23','95/05/24','95/05/25','95/05/26','95/05/27','95/05/28','95/05/29','95/05/30','95/05/31','95/06/01','95/06/02','95/06/03','95/06/04','95/06/05','95/06/06','95/06/07'],
    datasets: [
        {
            label: 'بازدید کننده',
            fill: false,
            data: ['23','42','14','9','8','10','6','7','13','46','20','16','24','16','19','17','21','31','19','12','12','15','15','16','22','13','18','19','56','16'],
            borderColor: 'rgba(255,153,51,0.5)',
            backgroundColor: 'rgba(255,153,51,0.5)',
            pointBorderColor: 'rgba(255,140,26,0.8)',
            pointBackgroundColor: 'rgba(255,140,26,1)',
            pointBorderWidth: 3,
        },
        {
        label: 'بازدید',
        data: ['66','33','71','23','25','45','31','33','46','85','55','28','38','22','52','36','51','10','24','20','20','21','17','39','37','15','22','52','72','35'],
        fill: false,
        borderColor: 'rgba(153,187,255,0.5)',
        backgroundColor: 'rgba(153,187,255,0.5)',
        pointBorderColor: 'rgba(102,153,255,0.8)',
        pointBackgroundColor: 'rgba(102,153,255,1)',
        pointBorderWidth: 3
    }]
},
    options: {
        responsive: true,
        title:{
            display:true,
            text:''
        },
        tooltips: {
            mode: 'label'
        },
        hover: {
            mode: 'dataset'
        },
        scales: {
            xAxes: [{
                display: true,
                stacked: true,
                scaleLabel: {
                    show: true,
                    labelString: 'Month'
                }
            }],
            yAxes: [{
                display: true,
                stacked: true,
                scaleLabel: {
                    show: true,
                    labelString: 'Value'
                },
                ticks: {
                    suggestedMin: 0,
                    // suggestedMax: chart_max_visit + (chart_max_visit / 5),
                }
            }]
        }
    }
};


var config2 = {
    type: 'pie',
    data: {
        datasets: [{
            data: [59,3,2,0,1,1],
            backgroundColor: [
                "#F46F5A",
                "#52B8DD",
                "#7E86E5",
                "#D0D6B3",
                "#E5C687",
                "#FFCC00",
            ],
        }],
        labels: [
            "google",
            "yahoo",
            "bing",
            "baidu",
            "aol",
            "ask"
        ]
    },
    options: {
        responsive: true,
        legend: {
            position: 'left'
        }
    }
};

var config3 = {
    type: 'horizontalBar',
    data: {
        labels: ["06 - 00", "12 - 06", "18 - 12", "24 - 18"],
        datasets: [{
            label: 'بازدید',
            backgroundColor: "rgba(153,187,255,0.5)",
            data: [299,604,668,229]
        }]

    },
    options: {
        // Elements options apply to all of the options unless overridden in a dataset
        // In this case, we are setting the border of each horizontal bar to be 2px wide and green
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [{
                display: true,
                stacked: true,
                scaleLabel: {
                    show: true,
                    labelString: 'Value'
                },
                ticks: {
                    suggestedMin: 0
                }
            }]
        }
    }
};

window.onload = function() {
    var ctx = document.getElementById("canvas").getContext("2d");
    window.myLine = new Chart(ctx, config);

    var ctx2 = document.getElementById("chart_pie").getContext("2d");
    window.myPie = new Chart(ctx2, config2);
    window.myPie.update();

    var ctx = document.getElementById("line").getContext("2d");
    window.line = new Chart(ctx,config3);
    window.line.update();
};