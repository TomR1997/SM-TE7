//
//  DisplayLoader.swift
//  Grocerly.iOS
//
//  Created by issd on 10/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit
import Lottie

    

class DisplayLoader {
    
    static let instance = DisplayLoader()
    
    let container: UIView
    
    private init (){
        self.container = UIView()
    }
    
    func displayLoader (onView: UIView){
        container.frame = onView.frame
        container.center = onView.center
        container.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.3)
        
        let animationView = LOTAnimationView(name: "icon_loading")
        animationView.loopAnimation = true
        animationView.contentMode = .scaleAspectFill
        animationView.center = onView.center
        
        container.addSubview(animationView)
        onView.addSubview(container)
        
        animationView.play()
    }
    
    func hideLoader (){
        container.removeFromSuperview()
    }
}

