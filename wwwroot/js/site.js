// wwwroot/js/site.js

window.initFlowChart = () => {
    let container = document.querySelector('.flow-chart');
    if (!container) return;

    let scale = 1;
    let pan = {
        x: 0,
        y: 0
    };
    let dragging = false;
    let lastPos = { x: 0, y: 0 };
    let lastClick = 0;

    container.addEventListener('wheel', function (e) {
        e.preventDefault();

        if (e.deltaY < 0) {
            scale += 0.1;
        } else {
            scale -= 0.1;
        }
        updateTransform();
    });

    container.addEventListener('mousedown', function (e) {
        if (e.button === 1) {
            let now = new Date().getTime();

            if (now - lastClick < 300) {
                fitToWindow();
            } else {
                dragging = true;
                lastPos = {
                    x: e.clientX,
                    y: e.clientY
                };
            }

            lastClick = now;
        }
    });

    window.addEventListener('mouseup', function () {
        dragging = false;
    });

    window.addEventListener('mousemove', function (e) {
        if (dragging) {
            pan.x += e.clientX - lastPos.x;
            pan.y += e.clientY - lastPos.y;
            lastPos = {
                x: e.clientX,
                y: e.clientY
            };
            updateTransform();
        }
    });

    function updateTransform() {
        container.style.transform = `translate(${pan.x}px, ${pan.y}px) scale(${scale})`;
    }

    function fitToWindow() {
        scale = 1;
        pan.x = 0;
        pan.y = 0;
        updateTransform();
    }
};
