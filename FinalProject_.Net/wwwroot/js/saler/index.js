
document.getElementById('datatable-search-input').addEventListener('keyup', function() {
        var searchQuery = this.value.toLowerCase();
        var table = document.querySelector('.datatable table');
    var rows = table.getElementsByTagName('tr');

        var matchFound = false;

        for (var i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
            var cells = rows[i].getElementsByTagName('td');
            var rowContainsQuery = false;

            for (var j = 0; j < cells.length; j++) {
                if (cells[j].textContent.toLowerCase().indexOf(searchQuery) > -1) {
                    rowContainsQuery = true;
                    break;
                }
            }

            if (rowContainsQuery) {
                rows[i].style.display = '';
                matchFound = true;
            } else {
                rows[i].style.display = 'none';
            }
        }



});



function confirmAction(event, element) {
    event.preventDefault(); 

    element.addEventListener('confirm.mdb.popconfirm', () => {
     
        const url = element.getAttribute('href'); 
        window.location.href = url;
    });
}

