//
//  Item.swift
//  Grocerly.iOS
//
//  Created by issd on 13/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class Item: Decodable {
    var id: String
    var imageUrl: String
    var name: String
    var price: Double
    var volume: String
    var creationDate: String
    var barcode: Int
    
    init(id: String, imageUrl: String, name: String, price: Double, volume: String, creationDate: String, barcode: Int){
        self.id = id
        self.imageUrl = imageUrl
        self.name = name
        self.price = price
        self.volume = volume
        self.creationDate = creationDate
        self.barcode = barcode
    }
}
