function addLink() {
    const titleInput = $("#newLinkTitle");
    const urlInput = $("#newLinkUrl");
    const imageInput = $("#newLinkImage")[0];

    const titleValidation = $("#newLinkTitleValidation");
    const urlValidation = $("#newLinkUrlValidation");
    const imageValidation = $("#newLinkImageValidation");

    titleValidation.text("").hide();
    urlValidation.text("").hide();
    imageValidation.text("").hide();

    let isValid = true;

    const title = titleInput.val().trim();
    if (!title) {
        titleValidation.text("Tytuł jest wymagany.").show();
        isValid = false;
    }

    const url = urlInput.val().trim();
    if (!url) {
        urlValidation.text("Adres URL jest wymagany.").show();
        isValid = false;
    } else if (!isValidUrl(url)) {
        urlValidation.text("Adres URL jest nieprawidłowy.").show();
        isValid = false;
    }

    if (imageInput && imageInput.files.length > 0 && !validateImage(imageInput.files[0], imageValidation)) {
        isValid = false;
    }

    if (!isValid) {
        return;
    }

    const formData = new FormData();
    formData.append("Title", title);
    formData.append("Url", url);

    if (imageInput.files.length > 0) {
        console.log(imageInput.files[0])
        formData.append("Image", imageInput.files[0]);
    }

    $.ajax({
        url: '/Settings/AddUserLink/',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (!response.isSuccess) {
                console.error(response.message);
                alert("Coś poszło nie tak.");
                return;
            }
            const newRow = $(response.data);

            const noLinksRow = $("#noLinks");
            if (noLinksRow.length > 0) {
                noLinksRow.remove();
            }

            $("#userLinksTable tbody").append(newRow);

            updateRowsNumeration();

            clearNewLinkInputs();
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Coś poszło nie tak.");
        }
    });
}

function saveLink(button) {
    const $button = $(button);
    const $row = $button.closest("tr");

    const id = $row.find("input[type='hidden'][name*='].Id']").val();
    const titleInput = $row.find("input[type='text'][name*='].Title']");
    const urlInput = $row.find("input[type='text'][name*='].Url']");
    const imageInput = $row.find("input[type='file']").get(0);


    const titleValidation = $row.find(`#title-validation-${$row.index()}`);
    const urlValidation = $row.find(`#url-validation-${$row.index()}`);
    const imageValidation = $row.find(`#image-validation-${$row.index()}`);

    titleValidation.text("").hide();
    urlValidation.text("").hide();
    imageValidation.text("").hide();

    let isValid = true;

    const title = titleInput.val().trim();
    if (!title) {
        titleValidation.text("Tytuł jest wymagany.").show();
        isValid = false;
    }

    const url = urlInput.val().trim();
    if (!url) {
        urlValidation.text("Adres URL jest wymagany.").show();
        isValid = false;
    } else if (!isValidUrl(url)) {
        urlValidation.text("Adres URL jest nieprawidłowy.").show();
        isValid = false;
    }

    if (imageInput && imageInput.files.length > 0 && !validateImage(imageInput.files[0], imageValidation)) {
        isValid = false;
    }

    if (!isValid) {
        return;
    }

    const formData = new FormData();
    formData.append("Id", id);
    formData.append("Title", title);
    formData.append("Url", url);

    if (imageInput && imageInput.files.length > 0) {
        formData.append("Image", imageInput.files[0]);
    }

    $.ajax({
        url: '/Settings/SaveUserLink/',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (!response.isSuccess) {
                console.error(response.message);
                alert("Coś poszło nie tak.");
                return;
            }

            console.log("Link saved successfully:", response);

            alert("Link został zapisany pomyślnie.");
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Coś poszło nie tak.");
        }
    });
}

function removeLink(button) {
    const $button = $(button);
    const $row = $button.closest("tr");
    const id = $row.find("input[type='hidden'][name*='].Id']").val();

    const confirmation = confirm("Czy na pewno chcesz usunąć ten link?");
    if (!confirmation) {
        return; 
    }

    console.log("Id of the object associated with this row:", id);

    if (id) {
        $.ajax({
            url: '/Settings/RemoveUserLink/',
            type: 'POST',
            data: { id: id },
            success: function (response) {
                if (response.isSuccess) {
                    $row.remove();
                    updateRowsNumeration();
                    if ($("#userLinksTable tbody tr").length === 0) {
                        $("#userLinksTable tbody").append(`
                            <tr id="noLinks" >
                                <td colspan="5" class="text-center">Brak linków.</td>
                            </tr>
                        `);
                    }
                } else {
                    console.error(response.message);
                    alert("Coś poszło nie tak.");
                }
            },
            error: function (xhr) {
                console.error(xhr.responseText);
                alert("Coś poszło nie tak.");
            }
        });
    }
}

function isValidUrl(string) {
    try {
        new URL(string);
        return true;
    } catch (_) {
        return false;
    }
}

function clearNewLinkInputs() {
    $("#newLinkTitle").val("");
    $("#newLinkUrl").val("");
    $("#newLinkImage").val("");
    $("#newHiddenImage").val("");

    const imagePreview = $("#newImagePreview");
    if (imagePreview) {
        imagePreview.attr("src", "");
        imagePreview.hide();
    }

    const noImageSpan = $("#newNoImageSpan");
    if (noImageSpan) {
        noImageSpan.show();
    }

    $("#newLinkTitleValidation").text("").hide();
    $("#newLinkUrlValidation").text("").hide();
    $("#newLinkImageValidation").text("").hide();
}

function updateRowsNumeration() {

    $("#userLinksTable tbody tr").each(function (index, row) {
        $(row).find("td:first").text(index + 1); +
            $(row).find("input, td, img, button, span").each(function () {
                const input = $(this);
                const nameAttr = input.attr("name");
                const idAttr = input.attr("id");

                if (nameAttr) {
                    input.attr("name", nameAttr.replace("Index", index));
                }

                if (idAttr) {
                    input.attr("id", idAttr.replace("Index", index));
                }
            });
    });
}

function previewImage(input) {
    const $row = $(input).closest("tr");
    const imagePreview = $row.find("img");
    const noImageSpan = $row.find(".text-muted");
    const hiddenImageInput = $row.find("input[type='hidden'][name*=ImageBase64]");
    const imageButton = $row.find("button.btn-primary");

    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            if (imagePreview) {
                imagePreview.attr("src", e.target.result).show();
            }

            if (noImageSpan) {
                noImageSpan.hide();
            }

            if (hiddenImageInput) {
                hiddenImageInput.val(e.target.result);
            }

            if (imageButton) {
                imageButton.text("Zmień obrazek");
            }
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function removeImage(button) {
    const $row = $(button).closest("tr");
    const imagePreview = $row.find("img");
    const noImageSpan = $row.find(".text-muted");
    const hiddenImageInput = $row.find("input[type='hidden'][name*=ImageBase64]");
    const fileInput = $row.find("input[type='file']").get(0);
    const imageButton = $row.find("button.btn-primary");

    if (imagePreview) {
        imagePreview.hide();
        imagePreview.attr("src", "");
    }

    if (noImageSpan) {
        noImageSpan.show();
    }

    if (hiddenImageInput) {
        hiddenImageInput.val("");
    }

    if (fileInput) {
        fileInput.value = "";
    }

    if (imageButton) {
        imageButton.text("Dodaj obrazek");
    }
}
function changeImage(button) {
    const $row = $(button).closest("tr");
    const fileInput = $row.find("input[type='file']").get(0);

    if (fileInput) {
        fileInput.click();
    }
}

function triggerFileInput(inputId) {
    const fileInput = document.getElementById(inputId);
    if (fileInput) {
        fileInput.click();
    }
}

function previewNewImage(input) {
    const imagePreview = document.getElementById("newImagePreview");
    const noImageSpan = document.getElementById("newNoImageSpan");
    const hiddenImageInput = document.getElementById("newHiddenImage");

    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            if (imagePreview) {
                imagePreview.src = e.target.result;
                imagePreview.style.display = "inline";
            }

            if (noImageSpan) {
                noImageSpan.style.display = "none";
            }

            if (hiddenImageInput) {
                hiddenImageInput.value = e.target.result;
            }
        };

        reader.readAsDataURL(input.files[0]);
    }
}

function removeNewImage() {
    const imagePreview = document.getElementById("newImagePreview");
    const noImageSpan = document.getElementById("newNoImageSpan");
    const hiddenImageInput = document.getElementById("newHiddenImage");
    const fileInput = document.getElementById("newLinkImage");

    if (imagePreview) {
        imagePreview.src = "";
        imagePreview.style.display = "none";
    }

    if (noImageSpan) {
        noImageSpan.style.display = "inline";
    }

    if (hiddenImageInput) {
        hiddenImageInput.value = "";
    }

    if (fileInput) {
        fileInput.value = "";
    }
}