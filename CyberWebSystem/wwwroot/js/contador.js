var timers = {};

function startTimer(id) {
    var timer = document.getElementById('timer-' + id);
    var time = 0;
    timers[id] = setInterval(function () {
        time++;
        var hours = Math.floor(time / 3600);
        var minutes = Math.floor((time % 3600) / 60);
        var seconds = time % 60;
        timer.textContent = (hours < 10 ? '0' + hours : hours) + ':' +
            (minutes < 10 ? '0' + minutes : minutes) + ':' +
            (seconds < 10 ? '0' + seconds : seconds);
    }, 1000);
}

function stopTimer(id) {
    clearInterval(timers[id]);
    var time = document.getElementById('timer-' + id).textContent;
    // Aquí puedes enviar 'time' al servidor y guardarlo en tu base de datos
}