//
//  ShoppingListCell.swift
//  Grocerly.iOS
//
//  Created by issd on 19/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class ShoppingListCell: UITableViewCell {
    
    var callback: ((_ mySwitch: UISwitch) -> Void)?
    var value: Bool = false
    
    @IBOutlet weak var ItemLabel: UILabel!
    @IBOutlet weak var itemView: UIImageView!
    @IBOutlet weak var itemSwitch: UISwitch!
    @IBAction func changedSwitchValue(sender: UISwitch) {
        self.value = sender.isOn
        callback?(sender)
    }
    
    
    
    override func awakeFromNib() {
        super.awakeFromNib()
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
    }
    
}
