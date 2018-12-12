//
//  ShoppingList.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class ShoppingList {
    var name: String
    var id: String
    var image: UIImage
    var distance: Int
    var shoppinglistItems: Int
    
    init(name: String, id: String, image: UIImage, distance: Int, shoppinglistItems: Int) {
        self.name = name
        self.id = id
        self.image = image
        self.distance = distance
        self.shoppinglistItems = shoppinglistItems
    }
    
}
