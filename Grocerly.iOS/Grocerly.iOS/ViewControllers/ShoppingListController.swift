//
//  LaunchController.swift
//  Grocerly.iOS
//
//  Created by issd on 06/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import Foundation
import Lottie

class ShoppingListController : UIViewController{
    
    var animationView : LOTAnimationView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        DisplayLoader.instance.displayLoader(onView: view,name: "icon_loading")
        
        DispatchQueue.main.asyncAfter(deadline: .now() + .seconds(2), execute: {
            DisplayLoader.instance.hideLoader()
            let item = Item(id: "kaas", imageUrl: "www.kaas.com/kaas.jpg", name: "kaas", price: 1337, volume: "550 gram", creationDate: "nu", barcode: 1234)
            UserPrefs().saveObject(forKey: "list_to_get", object: item, ofType: Item.self)
            
            let loadedItem = UserPrefs().loadObject(forKey: "list_to_get", ofType: Item.self)
        })
    }
}
