$(document).ready(function () {

    // HEADER

    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })


    //Load More
    let skip = 4;
    let productCount = $("#product-count").val();

    $(document).on('click', '#loadMore', function (e) {

        let productsList = $("#products-list");

        axios.get('/product/LoadMore?skip='+ skip)
            .then(function (response) {

                let datas = response.request.response;
                productsList.html(datas);

                if (skip>=productCount-4) {
                    skip = 0;
                }
                skip += 4;
            })
            .catch(function (error) {   
                // handle error
                console.log(error);
            });
        
        

        //$.ajax({
        //    url: "/product/Next",
        //    method: "get",
        //    success: function (res) {
        //        console.log(res);
        //    }
        //})
    })

    //search

    let searchData = $("#searchDataList");

    $("#input-search").keyup(function () {
       
        let value = $(this).val();
        $("#searchDataList li").slice(1).remove();

        axios.get('/home/SerachProduct?search=' + value)
            .then(function (response) {

                let datas = response.request.response;
                searchData.append(datas);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });
        
    });


    //Add to cart

    let AddCart = $(".addCart");

    AddCart.each(function () {
        $(this).on("click", function () {

            let dataId = $(this).attr("data-id");


            axios.get('/basket/additem?id=' + dataId)
                .then(function (response) {
                    $("#HeaderBasketCount").text(response.data.basketCount);
                    $("#HeaderSubTotal").text(response.data.subTotal);


                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                });


        });
    });

    //$(".item-minus").each(function () {
    //    $(this).on("click", function () {

    //        let dataId = $(this).attr("data-id");
            
    //       // $(this).prev().text("prev");

    //        axios.post('/basket/itemminus?id=' + dataId)
    //            .then(function (response) {
    //                $("#HeaderBasketCount").text(response.data.basketCount);
    //                $("#HeaderSubTotal").text(response.data.subTotal);

                   

    //                if (response.data.count > 1) {

    //                    $(this).next().text(response.data.count);
    //                }
    //                else {

    //                    $(this).parent().parent().parent().remove();
    //                }
    //            })
    //            .catch(function (error) {
    //                // handle error
    //                console.log(error);
    //            });


    //    });
    //});

    //$(".ItemPlus").each(function () {
    //    $(this).on("click", function () {

    //        let dataId = $(this).attr("data-id");


    //        axios.get('/basket/itemminus?id=' + dataId)
    //            .then(function (response) {
    //                $("#QuantityPr").text(response.data.count);
    //                $("#HeaderBasketCount").text(response.data.basketCount);
    //                $("#HeaderSubTotal").text(response.data.subTotal);

    //            })
    //            .catch(function (error) {
    //                // handle error
    //                console.log(error);
    //            });


    //    });
    //});

    function GetBasket(id){

        axios.get('/basket/getbasket?id=' + id)

            .then(function (response) {
                $("#QuantityPr").text(response.data.count);
                $("#HeaderBasketCount").text(response.data.basketCount);
                $("#HeaderSubTotal").text(response.data.subTotal);
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });
    }
    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})


let HeaderBasketCount = document.getElementById("HeaderBasketCount");
let HeaderSubTotal = document.getElementById("HeaderSubTotal");

let minusItem = document.querySelectorAll(".item-minus");
let plusItem = document.querySelectorAll(".item-plus");
let removeItem = document.querySelectorAll(".item-remove");

minusItem.forEach(m => {

    m.addEventListener("click", function () {

        let dataId = this.getAttribute('data-id');
        let parentBig = this.parentElement.parentElement.parentElement;
        let dataCount = this.parentElement.children[1];

        axios.post(`/basket/itemminus?id=${dataId}`)
            .then(function (response) {

                HeaderBasketCount.innerText=response.data.basketCount;
                HeaderSubTotal.innerText=response.data.subTotal;

              
                if (response.data.count ==0) {

                    parentBig.remove();
                    
                }
                else {
                    dataCount.innerText = response.data.count;
                    
                }
            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });

    })
})



plusItem.forEach(m => {



    m.addEventListener("click", function () {

        let dataId = this.getAttribute('data-id');
        let parentBig = this.parentElement.parentElement.parentElement;
        let dataCount = this.parentElement.children[1];


        axios.post(`/basket/itemplus?id=${dataId}`)
            .then(function (response) {

                HeaderBasketCount.innerText = response.data.basketCount;
                HeaderSubTotal.innerText = response.data.subTotal;

                dataCount.innerText = response.data.count;

            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });

    })
})


removeItem.forEach(m => {



    m.addEventListener("click", function () {

        let dataId = this.getAttribute('data-id');
        let parentBig = this.parentElement.parentElement.parentElement;
        let dataCount = this.parentElement.children[1];


        axios.post(`/basket/itemremove?id=${dataId}`)
            .then(function (response) {

                HeaderBasketCount.innerText = response.data.basketCount;
                HeaderSubTotal.innerText = response.data.subTotal;

                parentBig.remove();

            })
            .catch(function (error) {
                // handle error
                console.log(error);
            });

    })
})