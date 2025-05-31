(function () {

    const elementName = "#userTable";
    const columns = [
        {
            data: 'id', name: 'id', width: '30',
            render: function (key) {
                return `
                    <span data-key="${key}">
                        <a href="/admin/account/savedata?id=${key}" title="Edit"><i class="ri-pencil-line"></i></a>&nbsp;
                                                            <a href="#" class="btn-delete"><i class="ri-delete-bin-line" title="Delete" ></i></a>
                    </span>
                `;
            }
        },
        { data: 'userName', name: 'userName', autoWidth: true },
        { data: 'fullName', name: 'fullName', autoWidth: true },
        { data: 'email', name: 'email', autoWidth: true },
        { data: 'phone', name: 'phone', autoWidth: true },
        { data: 'isActive', name: 'isActive', autoWidth: true },
    ];

    const urlApi = "/admin/account/getaccountpagination";

    registerDataTable(elementName, columns, urlApi);

    $(document).on('click', '.btn-delete', function () {
        const key = $(this).closest('span').data('key');

        $.ajax({
            url: `/admin/account/delete/${key}`,
            dataType: 'json',
            method: 'POST',
            success: function (response) {
                if (!response) {
                    showToaster("Error", "Delete failed.");
                    return;
                }
                $(elementName).DataTable().ajax.reload();
                showToaster("Success","Delete successful,");
            }
        })
    });
})();