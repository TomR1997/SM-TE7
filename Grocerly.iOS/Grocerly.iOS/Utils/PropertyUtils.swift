//
//  PropertyUtils.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import Foundation


func getProperty(withKey: String) -> Any? {
    return UserDefaults.standard.value(forKeyPath: withKey)
}

func saveProperty(value: Any, withKey: String) -> () {
    UserDefaults.standard.set(value, forKey: withKey)
    UserDefaults.standard.synchronize()
}
