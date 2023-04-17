function filter(type) {
    let cards, i;
    let count = 0;
    cards = document.getElementsByClassName("card");
    buttons = document.getElementsByClassName("btn-filter");
    for (i = 0; i < cards.length; i++) {
        cards[i].parentElement.style.display = 'none';
        if (cards[i].classList.contains(type) || type === "all") {
            cards[i].parentElement.style.display = 'block';
            count += 1;
        };
    };
    for (i = 0; i < buttons.length; i++) {
        if (buttons[i].id == `btn-${type}`) {
            buttons[i].classList.remove("btn-sm");
            buttons[i].classList.add("btn-lg");
        }
        else {
            buttons[i].classList.remove("btn-lg");
            buttons[i].classList.add("btn-sm");
        }
    };
    if (type === "all") {
        document.getElementById("btn-all").classList.remove("btn-sm");
        document.getElementById("btn-all").classList.add("btn-lg");
    };
    if (count == 0) {
        document.getElementById("zeroHotel").style.display = 'block';
    }
    else {
        document.getElementById("zeroHotel").style.display = 'none';
    }
}