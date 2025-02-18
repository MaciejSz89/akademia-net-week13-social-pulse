function selectStyle(button) {
    const selectedStyle = $(button).data('style');

    const form = $('#styleForm');

    const token = form.find('input[name="__RequestVerificationToken"]').val();

    const formData = new FormData();
    formData.append("userLinkStyle", selectedStyle);
    formData.append("__RequestVerificationToken", token);

    $.ajax({
        url: '/Settings/SaveLinkStyle',
        method: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (!response.isSuccess) {
                console.error(response.message);
                alert("Coś poszło nie tak.");
                return;
            }

            const currentStyleButton = $('#currentStyleButton');
            currentStyleButton.attr('class', `btn btn-lg m-2 ${response.data}`);
            currentStyleButton.text('Aktualny Styl');

        },
        error: function (xhr, status, error) {
            console.error('Error saving style:', error);
            alert('Wystąpił błąd podczas zapisywania stylu.');
        }
    });
}


