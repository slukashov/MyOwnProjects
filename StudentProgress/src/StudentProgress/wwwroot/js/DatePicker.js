(function () {
    'use strict';

    $(document).on('ready', function () {
        $('#datepickerFrom').datepicker({
            format: 'yyyy-mm-dd',
            todayHighlight: true,
            endDate: '0d'
        });
    });
})();