using EFC_.Services;



//Skapar en instans av klassen 'MenuService' och anropar metoden 'MainMenu'
//'MenuService' tar hjälp av dens konstruktor och sparar den i variabeln 'menuService'. 
//Och sedan anropas 'MainMenu' av 'MenuService' med hjälp av 'await'
MenuService menuService = new();


await menuService.MainMenu();
