function showToaster(type, text, timeOut = 5000) {
    $.toast({
        heading: type,
        text: text,
        position: 'bottom-right',
        stack: false,
        icon: type === 'Information' ? 'info' : type.toLowerCase(),
        hideAfter: timeOut
    })
} 

//function mapObjectToControlView(modelView) {
//    if (typeof modelView !== 'object') {
//        return;
//    }

//    for (const property in modelView) {
//        if (modelView.hasOwnProperty(property)) {

//            const [firstCharacter, ...restChar] = property;

//            const capitalText = `${firstCharacter.toLocaleUpperCase()}${restChar.join('')}`;

//            $(`#${capitalText}`).val(modelView[property]);
//        }
//    }
//}

//window.mapObjectToControlView = mapObjectToControlView;