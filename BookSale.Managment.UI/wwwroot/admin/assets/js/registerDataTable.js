function registerDataTable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        //pageLength: 5,
        //lengthMenu: [5, 10, 20],
        //responsive: true,

        scrollY: 300,
        scrollX: true,
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json'
        }
    });
}