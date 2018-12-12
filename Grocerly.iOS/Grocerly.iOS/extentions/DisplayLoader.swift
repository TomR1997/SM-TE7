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
    
    func displayLoader (onView: UIView, name: String){
        container.frame = onView.frame
        container.center = onView.center
        container.backgroundColor = UIColor(red: 0, green: 0, blue: 0, alpha: 0.3)
        
        let whiteBG = UIView()
        whiteBG.frame = CGRect(x: 0, y: 0, width: 200, height: 200)
        whiteBG.center = onView.center
        whiteBG.backgroundColor = UIColor(named: "Background")
        whiteBG.layer.cornerRadius = 24
        whiteBG.layer.masksToBounds = true
        
        let animationView = LOTAnimationView(name: name)
        animationView.loopAnimation = true
        animationView.contentMode = .scaleAspectFill
        animationView.center = onView.center
        
        for view in container.subviews{
            view.removeFromSuperview()
        }
        
        container.addSubview(whiteBG)
        container.addSubview(animationView)
        
        onView.addSubview(container)
        
        animationView.play()
    }
    
    func hideLoader (){
        container.removeFromSuperview()
    }
}

