//
//  JobTableViewCell.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class JobTableViewCell: UITableViewCell {
    
    @IBOutlet weak var jobLabel : UILabel!
    @IBOutlet weak var avatarView: UIImageView!
    @IBOutlet weak var distanceView: UIImageView!
    @IBOutlet weak var distanceLabel: UILabel!
    @IBOutlet weak var itemsView: UIImageView!
    @IBOutlet weak var itemsLabel: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
    }

}
