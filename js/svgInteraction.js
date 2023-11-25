// wwwroot/js/svgInteractions.js 또는 유사한 파일에 이 코드를 추가하세요.
function addClickEventToSvgElement(svgElementId, dotNetReference, methodName) {
    const element = document.getElementById(svgElementId);
    if (element) {
        element.addEventListener('click', event => {
            dotNetReference.invokeMethodAsync(methodName, svgElementId);
        });
    }
}
