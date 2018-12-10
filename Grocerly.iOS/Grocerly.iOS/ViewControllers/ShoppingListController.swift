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
        DisplayLoader.instance.displayLoader(onView: view)
        
        DispatchQueue.main.asyncAfter(deadline: .now() + .seconds(4), execute: {
            DisplayLoader.instance.hideLoader()
        })
    }
}
