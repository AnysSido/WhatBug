$(function() {
    var activeBreadcrumb = $('.breadcrumb-item.active a');

    var sidebar = $('.nav-sidebar');
    var topLevelItems = sidebar.children('li');

    if (activeBreadcrumb) {
        if(matchText(activeBreadcrumb.text())) {
            return;
        }

        var nextBreadcrumb = $('.breadcrumb-item').not('.active').children('a');
        if (nextBreadcrumb) {
            nextBreadcrumb = nextBreadcrumb.last();
            matchText(nextBreadcrumb.text())
        }
    }

    function matchText(txt)
    {
        for (item of topLevelItems) {
            var parent = $(item);
            var childrenContainer = parent.children('ul');

            if (childrenContainer[0]) {
                var children = $(childrenContainer).children('li');
                
                for (child of children) {
                    var anchor = $(child).find('a');
                    var text = anchor.children('p').text();
                    if (text == txt) {
                        anchor.addClass('active');
                        parent.addClass('menu-open');
                        parent.children('a').addClass('active');
                        return true;
                    }
                };
            } else {
                var anchor = parent.find('a');
                var text = anchor.children('p').text();
                if (text == txt) {
                    anchor.addClass('active');
                    return true;
                }
            }
        };
        return false;
    }
})