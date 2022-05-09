var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#search").autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    url: "/Book/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (data) {
                        console.log(data.data)
                        response(data.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#search").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#search").val(ui.item.label);
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $('<li>')
                .append("<a>" + item.label + "</a>")
                .appendTo(ul);
        };
    }
}
common.init();