function UpdateArtists(sender,usr_id) {

    $(sender).attr('disabled', true)
    let s = $(sender).text()
    $(sender).text('Обновление...')
    
    

    $.ajax({
        type: 'Get',
        url: '/Index/UpdateArtists?usr_id=' + usr_id,
        success: function (answer) {

            if (answer == 'OK') {
                let s_ok = ' (Обновлено)'
                $(sender).attr('disabled', false)
                $(sender).text(s + (s.includes(s_ok)?'':s_ok))
            } else {
                alert('Кажется что-то пошло не так. ' + answer)
                $(sender).attr('disabled', false)
                $(sender).text = s

            }

        }
    })
}


function LoadNewAlbums(sender, usr_id) {
    $(sender).attr('disabled', true)
    $.ajax({
        type: 'Get',
        url: '/Index/LoadNewAlbums?usr_id=' + usr_id,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (data) {
            $(sender).attr('disabled', false)
            if (data == "ERROR") {
                alert('Кажется что-то пошло не так. ')
            }
            else {
                console.log(data)
                let html = GetHTMLForItems(data)
                $("#albums").html(html)
                $(sender).attr('disabled', false)
            }
            
            

        }
    })






}

function al ()
    {

    alert("Hello from al")
}

function GetHistoryUrl( user_id, access_token,index=0) {
    return '/Index/GetHistory?user_id=' + user_id + '&access_token=' + access_token+'&index='+index
}

function ConsoleLog(o) {
    console.log(o)
}

function GetHistory(sender, user_id, access_token) {
    
    $(sender).attr('disabled', true)
    $.ajax({
        type: 'Get',
        url: '/Index/GetHistory?user_id=' + user_id + '&access_token=' + access_token,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (data) {            
            $(sender).attr('disabled', false)
            if (data == "ERROR") {
                alert('Кажется что-то пошло не так. ')
            }
            else {
                console.log(data)
                //let html = GetHTMLForItems(data)
                //$("#history_items").val(data.next)
                
                $(sender).attr('disabled', false)
               
            }



        }
    })






}


function GetHTMLForItems(result) {
    let s = '';
    for (let i = 0; i < result.length; i++) {
        s += '<div class="row">'

        //pict        
        s += '<div class="col">'
        s += '<a href="' + result[i].Item1.link + '" target="_blank"><img src="' + result[i].Item1.picture_small+'"></a></div>';


        //name        
        s += '<div class="col">'
        s += '<a href="' + result[i].Item1.link + '" target="_blank">' + result[i].Item1.name + '</a></div>';

        //album pict
        s += '<div class="col">'
        s += '<a href="' + result[i].Item2.link + '" target="_blank"><img src="' + result[i].Item2.cover_small + '"></a></div>';

        //album title
        s += '<div class="col">'
        s += '<a href="' + result[i].Item2.link + '" target="_blank">' + result[i].Item2.title + '</a></div>';

        //release date
        s += '<div class="col">'
        s += result[i].Item2.release_date + '</div>';


        s += '</div>'
    }
    return s;

}
