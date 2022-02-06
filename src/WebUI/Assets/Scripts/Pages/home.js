$(function() {    
    $(".dial").knob({
        max: 100,
        min: 0,
        readOnly: true,
        width: 160,
        height: 110,
        fgColor: 'rgba(255, 255, 255, 0.7)',
        bgColor: 'rgba(255, 255, 255, 0.2)',
        thickness: 0.25,
        angleArc: 220,
        angleOffset: 250,
        format : function (value) {
            return value + '%';
        }
    });
});